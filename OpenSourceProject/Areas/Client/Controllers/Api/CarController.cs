using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSourceProject.Data;
using OpenSourceProject.Models;
using OpenSourceProject.Areas.Client.Models.ViewModel;
using OpenSourceProject.Areas.Client.Models.ResourceModels;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenSourceProject.Helpers;
using OpenSourceProject.Models.ResourceModels;

namespace OpenSourceProject.Areas.Client.Controllers.Api
{
    [Route("api/client/[controller]")]
    [ApiController]
   
    public class CarController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly Error _error;
        

        public CarController
            (ApplicationDbContext context
            ,Error error

            )
        {
            _db = context;
            _error = error;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult> Getcars()
        {
          if (_db.cars == null)
          {
              return NotFound(new Error
              {
                  Message="table of cars is not found",
                  ErrorCode="404"

              });
          }
            List<Car> cars = await _db.cars.ToListAsync();
            return Ok(new ResultResponse <List<Car>> { 
                Data=cars,
                Status="200"
            });
        }

        // GET: api/Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCar(string id)
        {
          if (_db.cars == null)
          {
              return NotFound(new Error
              {
                  Message = "table of cars is not found",
                  ErrorCode = "404"

              });
          }
            var car = await _db.cars.FirstOrDefaultAsync(c=>c.Id==id);

            if (car == null)
            {
                return NotFound(new Error
                {
                    Message = "this car is not found ,please put correct id",
                    ErrorCode = "404"

                });
            }

            return Ok(new ResultResponse<Car>
            {
                Data=car,
                Status="200"
            });
        }

        // PUT: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<Car>> PutCar(CarVM car)
        {
            Car dbCar = await _db.cars.FindAsync(car.Id);
            if (dbCar == null)
            {
                return NotFound(new Error
                {
                    Message = "this car is not found ,please put correct id",
                    ErrorCode = "404"

                });
            }
            else
            {
                dbCar.Name = car.Name;
                dbCar.ChassisNumber = car.ChassisNumber;
                dbCar.CarModel = _db.carModels.FirstOrDefault(c => c.Id == car.CarModelId);
                dbCar.CarType = _db.carTypes.FirstOrDefault(c => c.Id == car.CarTypeId);
                dbCar.UpdatedAt = DateTime.Now;

            _db.cars.Update(dbCar);
                _db.SaveChanges(); 


            return Ok(new ResultResponse<Car>
            {
                Data=dbCar,
                Status="200"
            });
            }

          //  _db.Entry(car).State = EntityState.Modified;

          

        }

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarVM car)
        {
          if (_db.cars == null)
          {
              return NotFound(new Error
              {
                  Message = "table of cars is not found",
                  ErrorCode = "404"

              });

          }
            Car newCar = new Car()
            {
                
                Name = car.Name,
                ChassisNumber = car.ChassisNumber,
                CarModel = _db.carModels.FirstOrDefault(c=>c.Id== car.CarModelId),
                CarType= _db.carTypes.FirstOrDefault(c=>c.Id==car.CarTypeId),
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now
                
            };

           _db.cars.Add( newCar);
            await _db.SaveChangesAsync();



            return Created("..", new ResultResponse<Car>
            {
                Data = newCar,
                Status = "201"

            });
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            if (_db.cars == null)
            {
                return NotFound(new Error
                {
                    Message = "table of cars is not found",
                    ErrorCode = "404"

                });
            }
            var car = await _db.cars.FindAsync(id);
            if (car == null)
            {
                return NotFound(new Error
                {
                    Message = "this car is not found ,please put correct id",
                    ErrorCode = "404"

                });
            }

            _db.cars.Remove(car);
            await _db.SaveChangesAsync();

            return Ok();
        }

        //private bool CarExists(string id)
        //{
        //    return (_db.cars?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
