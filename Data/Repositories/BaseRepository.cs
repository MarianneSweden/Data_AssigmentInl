using System.Linq.Expressions;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<T> : Data.Interfaces.IBaseRepository<T> where T : class
    {
        protected readonly DataContext _context; 
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Hämta alla 
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Hämta ID
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Hämta en valfri post
        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        // Lägg till 
        //public virtual async Task AddAsync(T entity)
        //{
        //  await _context.Set<T>().AddAsync(entity);
        //await _context.SaveChangesAsync();
        //}

        //Chatgpt istället för den lägg till över, det sparas ner men jag får ändå ett fel när jag sparar ner projektet... Nåt med ID att göra
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); // Använd _dbSet istället för _context.Set<T>()
            await _context.SaveChangesAsync();
        }



        // Uppdatera (gör metoden virtual)
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Ta bort en post
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
