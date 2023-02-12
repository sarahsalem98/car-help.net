using OpenSourceProject.Data;
using OpenSourceProject.Repository.IRepository;

namespace OpenSourceProject.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        public ICarRepository car { get;  private set; }

        private readonly ApplicationDbContext _db;

        public UniteOfWork(ApplicationDbContext db) {
            _db = db;
            car = new CarRepository(_db);
        
        }

        public  void Save()
        {
            _db.SaveChanges();
        }
    }
}
