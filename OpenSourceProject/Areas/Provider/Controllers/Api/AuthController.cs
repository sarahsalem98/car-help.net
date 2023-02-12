//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging;
using OpenSourceProject.Areas.Client.Models.ResourceModels;
using OpenSourceProject.Areas.Provider.Models.ResourceModels;
using OpenSourceProject.Data;
using OpenSourceProject.Helpers;
using OpenSourceProject.Models;
using OpenSourceProject.Models.ResourceModels;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = OpenSourceProject.Helpers.Error;
using ProviderModel = OpenSourceProject.Models.Provider;
using Service = OpenSourceProject.Helpers.Service;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenSourceProject.Areas.Provider.Controllers.Api
{
    [Route("api/provider")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _provider;
        private readonly RoleManager<IdentityRole> _role;
        private readonly ApplicationDbContext _db;
        private readonly Auth _auth;
        private readonly Service _service;
        private readonly Error _error;
        private readonly Roles _roles;
        private readonly JwtService _jwtService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string[] nextStepvalues = { "Services", "brands", "location", "workhours", "finished" };

        public AuthController
            (
            ApplicationDbContext db,
            UserManager<ApplicationUser> provider,
            RoleManager<IdentityRole> role,
            Auth auth,
            Error error,
            Roles roles,
            JwtService jwtService,
            IWebHostEnvironment webHost,
            Service service

            )
        {
            _db = db;
            _role = role;
            _error = error;
            _roles = roles;
            _auth = auth;
            _jwtService = jwtService;
            _provider = provider;
            _webHostEnvironment = webHost;
            _service = service;
        }
        //  private readonly App

        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //  public async Task<ActionResult> firstStepRegister()
        //  {

        //  }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationProviderResponse>> accountRegisterFirstStep([FromForm] RegisterProviderAccountRequest registerProviderRequest, IFormFile WorkShopPhoto, IFormFile RegisterationFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProviderModel DbProvider = _db.providers.FirstOrDefault(p => p.PhoneNumber == registerProviderRequest.PhoneNumber);
            if (DbProvider == null)
            {


                ProviderModel provider = new ProviderModel();

                provider.UserName = Guid.NewGuid().ToString();
                provider.PhoneNumber = registerProviderRequest.PhoneNumber;
                provider.PasswordHash = _auth.HashPassword(registerProviderRequest.Password);
                provider.WorkShopPhotoUrl = _service.AddFilesToProvider(_webHostEnvironment, WorkShopPhoto, "workShopPhoto", provider);
                provider.RegisterationFile = _service.AddFilesToProvider(_webHostEnvironment, RegisterationFile, "registerationFile", provider);
                provider.EngineerName = registerProviderRequest.EngineerName;
                provider.WhatsAppNumber = registerProviderRequest.WhatsAppNumber;
                provider.NextStep = nextStepvalues[0];
                provider.WorkShopName = registerProviderRequest.WorkShopName;

                var result = await _provider.CreateAsync(provider);

                if (!result.Succeeded)
                {
                    return BadRequest(_error.signError(result.Errors));
                }
                if (!await _role.RoleExistsAsync(_roles.Provider.ToString()))
                {
                    await _role.CreateAsync(_roles.Provider);
                }


                await _provider.AddToRoleAsync(provider, _roles.Provider.ToString());
                var token = await _jwtService.CreateTokenForProvider(provider, _roles.Provider.ToString());
                return Ok(token);



            }
            else
            {
                return BadRequest(new Error
                {
                    ErrorCode = "400",
                    Message = "phone number is already taken ,please log in"
                });
            }


        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationClientResponse>> Login([FromForm] AuthenticationProviderRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProviderModel user = _db.providers.FirstOrDefault(u => u.PhoneNumber == loginRequest.PhoneNumber);
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
                    var token = await _jwtService.CreateTokenForProvider(user, _roles.Provider.ToString());
                    return Ok(token);
                }
            }

        }

        [Authorize(Roles = "Provider")]
        [HttpPost]
        [Route("register/service")]
        public async Task<ActionResult<ResultResponse<ProviderModel>>> RegisterProviderServices([FromForm] List<string> services)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);
            if (provider != null)
            {
                // List< ProviderService> providerServices = new List<ProviderService> ();
                provider.ProviderServices = new List<ProviderService>();
                provider.NextStep = nextStepvalues[1];
                provider.UpdatedAt = DateTime.Now;
                foreach (var service in services)
                {
                    provider.ProviderServices.Add(new ProviderService
                    {
                        ProviderId = userId,
                        SubServiceId = service,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });
                }
                _db.providers.Update(provider);
                await _db.SaveChangesAsync();
                return Ok(new ResultResponse<ProviderModel>
                {
                    Status = "200",
                    Data = provider
                });

            }
            else
            {
                return Unauthorized(new Error
                {
                    ErrorCode = "400",
                    Message = "this user is not registerd "
                });
            }
        }
        [Authorize(Roles = "Provider")]
        [HttpPost]
        [Route("register/brand")]
        public async Task<ActionResult<ResultResponse<ProviderModel>>> RegisterProviderbrands([FromForm] List<string> brands)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);
            if (provider != null)
            {

                provider.ProviderBrands = new List<ProviderBrand>();
                provider.UpdatedAt = DateTime.Now;
                provider.NextStep = nextStepvalues[2];
                foreach (var brand in brands)
                {
                    provider.ProviderBrands.Add(new ProviderBrand
                    {
                        Provider = provider,
                        Brand = _db.brands.FirstOrDefault(b => b.Id == brand),

                    });
                }

                _db.providers.Update(provider);
                await _db.SaveChangesAsync();
                return Ok(new ResultResponse<ProviderModel>
                {
                    Status = "200",
                    Data = provider
                });


            }
            else
            {
                return Unauthorized(new Error
                {
                    ErrorCode = "400",
                    Message = "this user is not registerd "
                });
            }

        }
        [Authorize(Roles = "Provider")]
        [HttpPost]
        [Route("register/location")]

        public async Task<ActionResult<ResultResponse<ProviderModel>>> RegisterProviderLocation([FromForm] RegisterProviderLocationRequest locationRequest)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);
            if (provider != null)
            {
                if (_db.addresses.Any(a => a.UserId == userId))
                {
                    return Ok (new Error
                    {
                        ErrorCode = "500",
                        Message = "this user is only need one address"
                    });
                }  

                provider.providerAddress = new UserAddress();
                // provider.providerAddress.Id = Guid.NewGuid().ToString();
                // provider.ProviderLocation.ProviderId = userId;
                // provider.providerAddress. = locationRequest.CityId;
                provider.providerAddress.Lat = locationRequest.Lat;
                provider.providerAddress.Long = locationRequest.Lng;
                provider.providerAddress.Address = locationRequest.Address;
                provider.providerAddress.Name = locationRequest.Name;
                provider.providerAddress.PhoneNumber = locationRequest.PhoneNumber;
                provider.providerAddress.Note = locationRequest.Note;
                provider.providerAddress.UpdatedAt = DateTime.Now;
                provider.providerAddress.CreatedAt = DateTime.Now;
                provider.NextStep = nextStepvalues[3];
                provider.UpdatedAt = DateTime.Now;
                _db.providers.Update(provider);
                await _db.SaveChangesAsync();
                return Ok(new ResultResponse<ProviderModel>
                {
                    Status = "200",
                    Data = provider
                });


            }
            else
            {
                return Unauthorized(new Error
                {
                    ErrorCode = "400",
                    Message = "this user is not registerd "
                });

            }

        }
        [Authorize(Roles = "Provider")]
        [HttpPost]
        [Route("register/workhours")]
        public async Task<ActionResult<ResultResponse<ProviderModel>>> RegisterProviderWorkHours([FromForm] List<string> workhours)
        {


            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);


            if (provider != null)
            {
                provider.ProviderWorkHours = new List<ProviderWorkHour>();
                foreach (var workhour in workhours)
                {
                    var workHour = JsonConvert.DeserializeObject<RegisterProviderWorkHoursRequest>(workhour);
                    if (workHour.From >= workHour.To)
                    {


                        return BadRequest(new Error
                        {
                            ErrorCode = "400",
                            Message = "time from should be less than time to"
                        });
                    }
                    provider.ProviderWorkHours.Add(new ProviderWorkHour
                    {
                        Id = Guid.NewGuid().ToString(),
                        Day_AR = workHour.Day_AR,
                        Day_EN = workHour.Day_EN,
                        From = workHour.From,
                        To = workHour.To,
                        IsClosed = workHour.IsClosed,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now

                    });


                }
                provider.NextStep = nextStepvalues[4];
                provider.UpdatedAt = DateTime.Now;
                _db.providers.Update(provider);
                await _db.SaveChangesAsync();
                return Ok(new ResultResponse<ProviderModel>
                {
                    Status = "200",
                    Data = provider
                });
            }
            else
            {
                return Unauthorized(new Error
                {
                    ErrorCode = "400",
                    Message = "this user is not registerd "
                });
            }



        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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
