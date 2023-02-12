using OpenSourceProject.Data;
using OpenSourceProject.Models;
using OpenSourceProject.Repository.IRepository;

namespace OpenSourceProject.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly ApplicationDbContext _db;
        public CarRepository(ApplicationDbContext db) : base(db)
        {
            _db= db;    
        }

        public void Update(Car car)
        {
            _db.cars.Update(car);
        }
    }
}
