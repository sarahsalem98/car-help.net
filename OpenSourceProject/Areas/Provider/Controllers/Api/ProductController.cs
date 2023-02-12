using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceProject.Areas.Provider.Models.ResourceModels;
using OpenSourceProject.Data;
using OpenSourceProject.Helpers;
using OpenSourceProject.Models;
using OpenSourceProject.Models.ResourceModels;
using System.Collections.Generic;
using System.Security.Claims;
using ProviderModel = OpenSourceProject.Models.Provider;
using Service = OpenSourceProject.Helpers.Service;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenSourceProject.Areas.Provider.Controllers.Api
{



    [Route("api/provider/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly Service _service;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public ProductController(
            ApplicationDbContext dbContext,
            Service service,
            IWebHostEnvironment webHostEnvironment

            )
        {
            _db = dbContext;
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/<ProductController>
        [Authorize(Roles = "Provider")]
        [HttpGet]
        public ActionResult<ResultResponse<IList<Product>>> getProducts()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);
            List<Product> products = _db.Product.Where(p => p.ProviderId == userId).ToList();
            if (provider != null)
            {
                return Ok(new ResultResponse<IList<Product>>
                {
                    Status = "200",
                    Data = products
                });
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<ProductController>/5
        [Authorize(Roles = "Provider")]
        [HttpGet("{id}")]

        public async Task<ActionResult<ResultResponse<Product>>> getProductbyId(string id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(new ResultResponse<Product>
            {
                Data = _db.Product.Where(p => p.ProviderId == userId).Where(p => p.Id == id).FirstOrDefault(),
                Status = "200"
            });


        }

        // POST api/<ProductController>
        [Authorize(Roles = "Provider")]
        [HttpPost]
        public async Task<ActionResult<ResultResponse<Product>>> addProduct([FromForm] AddProductRequest addProductRequest)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);
            Category categor = _db.categories.FirstOrDefault(c => c.Id == addProductRequest.CategoryId);
            if (provider != null)
            {
                if (categor == null)
                {
                    return NotFound(new Error
                    {
                        ErrorCode = "404",
                        Message = "this category id is not found in categoruy table"
                    });
                }
                Product product = new Product();

                product.Id = Guid.NewGuid().ToString();
                // CategoryId = addProductRequest.CategoryId,
                product.Category = categor;
                product.ProviderId = userId;
                product.Name = addProductRequest.Name;
                product.Description = addProductRequest.Description;
                product.PriceBeforeDiscount = addProductRequest.PriceBeforeDiscount;
                product.PriceAfterDiscount = addProductRequest.PriceAfterDiscount;
                product.Qty = addProductRequest.Qty;
                product.Image = _service.AddFilesToProduct(_webHostEnvironment, addProductRequest.Image, product);

                


                _db.Product.Add(product);
                _db.SaveChanges();
               // _db.SaveChanges();
                await _db.SaveChangesAsync();
                return Ok(new ResultResponse<Product>
                {
                    Data = product,
                    Status = "201"
                });
            }
            else
            {
                return NotFound(new Error
                {
                    Message = "this provider is not loged in",
                    ErrorCode = "400"
                });
            }
        }

        // PUT api/<ProductController>/5
        [Authorize(Roles = "Provider")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultResponse<Product>>> Put(string id, [FromForm] AddProductRequest addProductRequest)
        {

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProviderModel provider = _db.providers.FirstOrDefault(p => p.Id == userId);
            Category categor = _db.categories.FirstOrDefault(c => c.Id == addProductRequest.CategoryId);

            if (provider != null)
            {
                Product product = _db.Product.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.Category = categor;
                    product.ProviderId = userId;
                    product.Name = addProductRequest.Name;
                    product.Description = addProductRequest.Description;
                    product.PriceBeforeDiscount = addProductRequest.PriceBeforeDiscount;
                    product.PriceAfterDiscount = addProductRequest.PriceAfterDiscount;
                    product.Qty = addProductRequest.Qty;
                    product.Image = _service.AddFilesToProduct(_webHostEnvironment, addProductRequest.Image, product);
                    return Ok(product);

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }


        }

       // DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(string id)
        //{


        //}
    }
}
