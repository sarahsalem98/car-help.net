using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceProject.Helpers;
using OpenSourceProject.Models;
using OpenSourceProject.Models.ResourceModels;
using OpenSourceProject.Helpers;
using Microsoft.AspNet.Identity;
using ClientModel = OpenSourceProject.Models.Client;
using OpenSourceProject.Data;
using OpenSourceProject.Areas.Client.Models.ResourceModels;
using Service = OpenSourceProject.Helpers.Service;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenSourceProject.Areas.Client.Controllers.Api
{
    [Route("api/client")]
    [Authorize(Roles = "Client")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _client;
        private readonly ApplicationDbContext _db;
        private readonly Service _service;
        private readonly IWebHostEnvironment _webHost;
        public OrderController (
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> client,
            ApplicationDbContext dbContext,
            Service service,
            IWebHostEnvironment webHost
            
            )
        {
            _client = client;
            _db = dbContext;
            _service = service;
            _webHost = webHost;
        }

    
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [Authorize(Roles ="Client")]
        [HttpPost]
        [Route("order")]
        public async Task<ActionResult<ResultResponse<Order>>> makePublicPrivateOrder([FromForm] PublicPrivateOrderRequst publicPrivateOrderRequst)
        {
            var userId = this.User.getAuthenticatedUser();
              if( _db.clients.Any(c => c.Id == userId))
            {


                Order order = new Order();

                order.CarId = publicPrivateOrderRequst.CarId;
                order.Status = orderStatus.New;
                order.Type = publicPrivateOrderRequst.ProviderId == null ? orderType.Public : orderType.Private;
                order.Description = publicPrivateOrderRequst.Description;
                order.ProviderId = publicPrivateOrderRequst.ProviderId;
                order.ClientId = userId;
                order.Image =  _service.AddFilesToOrder(_webHost, publicPrivateOrderRequst.Image, order);
                order.CreatedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;

                    
                    await _db.orders.AddAsync(order);
                    _db.SaveChanges();
                    return Ok(new ResultResponse<Order>
                    {
                        Data = order,
                        Status = "201"
                    });



            



            }
            else
            {
                return Unauthorized();
            }


        }


        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
