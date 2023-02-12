using System.Linq.Expressions;

namespace OpenSourceProject.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {

        Task  Add (T entity);    
        void Remove(T entity);  
        void RemoveRange(IEnumerable<T> entities);

         Task< IEnumerable<T> >  GetAll (Expression<Func<T, bool>>? filter= null, string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
    }
}
