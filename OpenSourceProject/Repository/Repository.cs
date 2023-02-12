using Microsoft.EntityFrameworkCore;
using OpenSourceProject.Data;
using OpenSourceProject.Repository.IRepository;

namespace OpenSourceProject.Repository
{
    public class Repository <T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); 
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<  IEnumerable<T> >  GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);    
            }
            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                  query= query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
