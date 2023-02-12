
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenSourceProject.Models;
using OpenSourceProject.Areas.Client.Models.ResourceModels;
using ClientModel = OpenSourceProject.Models.Client;
using OpenSourceProject.Helpers;
using OpenSourceProject.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OpenSourceProject.Areas.Client.Models.ViewModel;
using OpenSourceProject.Models.ResourceModels;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenSourceProject.Areas.Client.Controllers.Api
{
    [Route("api/client")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _client;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _role;
        private readonly Auth _auth;
        private readonly Error _error;
        private readonly Roles _roles;
        private readonly ApplicationDbContext _db;
        private readonly JwtService _jwtService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(UserManager<ApplicationUser> client
            , RoleManager<IdentityRole> role
            , Auth auth
            , Error error
            , Roles roles
            , JwtService jwtService
            , ApplicationDbContext db
            , IWebHostEnvironment webHostEnvironment)
        {
            _client = client;
            _role = role;
            _auth = auth;
            _error = error;
            _roles = roles;
            _jwtService = jwtService;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: api/<AuthController>
        [Authorize(Roles = "Provider")]
        [HttpGet]
        [Route("cities")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationClientResponse>> Register([FromBody] RegisterClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClientModel user = _db.clients.FirstOrDefault(u => u.PhoneNumber == clientRequest.PhoneNumber);
            if (user == null)
            {
                ClientModel clientModel = new ClientModel {
                    UserName = Guid.NewGuid().ToString()
                 , PasswordHash = _auth.HashPassword(clientRequest.Password)
                 , CityId = clientRequest.CityId
                 , PhoneNumber = clientRequest.PhoneNumber
                 , Name = clientRequest.Name
                 , CreatedAt = DateTime.Now
                 , UpdatedAt = DateTime.Now
                };

                var result = await _client.CreateAsync(clientModel);
                if (!result.Succeeded)
                {
                    return BadRequest(_error.signError(result.Errors));
                }
                if (!await _role.RoleExistsAsync(_roles.Client.ToString()))
                {
                    await _role.CreateAsync(_roles.Client);

                }


                await _client.AddToRoleAsync(clientModel, _roles.Client.ToString());
                var token = await _jwtService.CreateTokenForClient(clientModel, _roles.Client.ToString());
                return Ok(token);
            }
            else {

                return BadRequest(new Error
                {
                    ErrorCode = "400",
                    Message = "phone number is already taken ,please log in"
                });
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationClientResponse>> Login([FromBody] AuthenticationClientRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClientModel user = _db.clients.FirstOrDefault(u => u.PhoneNumber == loginRequest.PhoneNumber);
            if (user == null)
            {
                return BadRequest(new Error
                {
                    ErrorCode = "400",
                    Message = "this phone number is not registered in the system , please register first"
                });
            }
            else
            {
                if (!_auth.ValidatePassword(user.PasswordHash, loginRequest.Password))
                {
                    return BadRequest(new Error
                    {
                        ErrorCode = "400",
                        Message = "password you entered is not correct "
                    });
                }
                else
                {
                    var token = await _jwtService.CreateTokenForClient(user, _roles.Client.ToString());
                    return Ok(token);
                }
            }

        }
        // [Authorize(Roles ="Client")]
        [HttpGet]
        [Route("refresh")]
        public async Task<ActionResult<AuthenticationClientResponse>> RefreshToken()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userRole = claimIdentity.FindFirst(ClaimTypes.Role).Value;
                ClientModel user = _db.clients.FirstOrDefault(u => u.Id == userId);
                var token = await _jwtService.CreateTokenForClient(user, userRole);

                return Ok(token);

            }
            else
            {
                return BadRequest(new Error
                {
                    Message = "this user is not authenticated ,you should  login first ",
                    ErrorCode = "400"
                });
            }
        }
        [HttpPost]
        [Route("update")]

        public async Task<ActionResult<ResultResponse<ClientModel>>> UpdateProfile([FromForm] UpdateClientRequest updateClientRequest, IFormFile file)
        {
            if (ModelState.IsValid)
            {


                if (User.Identity.IsAuthenticated)
                {
                    var claimIdentity = (ClaimsIdentity)User.Identity;
                    var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userRole = claimIdentity.FindFirst(ClaimTypes.Role).Value;
                    ClientModel user = _db.clients.Where(u => u.Id == userId).FirstOrDefault();




                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images\clients");
                        var extention = Path.GetExtension(file.FileName);
                        if (user.ProfilePhotoUrl != null)
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, user.ProfilePhotoUrl.Trim('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }

                        }
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        user.ProfilePhotoUrl = @"images\clients\" + fileName + extention;
                    }
                    user.Name = updateClientRequest.Name;
                    user.PhoneNumber = updateClientRequest.PhoneNumber;
                    user.CityId = updateClientRequest.CityId ?? 0;
                    user.UpdatedAt = DateTime.Now;
                    _db.clients.Update(user);
                    // _db.Entry(user.City).State = EntityState.Modified;
                    _db.SaveChanges();


                    return Ok(new ResultResponse<ClientModel>
                    {
                        Status = "200",
                        Data = _db.clients.Include(c => c.City).FirstOrDefault(c => c.Id == userId)
                    });



                }
                else
                {
                    return BadRequest(new Error
                    {
                        ErrorCode = "400",
                        Message = "this user is not authenticated ,please login"
                    });
                }
            }
            else
            {
                return BadRequest();
            }
         
        } 
        //public async Task<ActionResult<ClientModel>> resetPassword()
        //{

        //}
        
    
            



        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
  
}
