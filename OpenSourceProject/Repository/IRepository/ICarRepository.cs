using OpenSourceProject.Models;

namespace OpenSourceProject.Repository.IRepository
{
    public interface ICarRepository:IRepository<Car>
    {
        void Update(Car car);
    }
}
