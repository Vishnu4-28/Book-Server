
using E_commerce.Server.data;
using E_commerce.Server.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Server.DAL.BASE
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly ApplicationDbContext _Dbcontext;
        protected readonly DbSet<T> _DbSet;

        public Repository(ApplicationDbContext _context)
        {

            _Dbcontext = _context;
            _DbSet = _Dbcontext.Set<T>();

        }


        public async Task Add(T entity)
        {
            _DbSet.Add(entity);
           await  _Dbcontext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _DbSet.Remove(entity);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {

            return await _DbSet.ToListAsync();

        }

        public async Task<T> GetById(int id)
        {
            var entity = await _DbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            return entity;
        }

        public Task Update(T entity)
        {
            _Dbcontext.Entry(entity).State = EntityState.Modified;
            return _Dbcontext.SaveChangesAsync();
        }

        public async Task<T> GetByEmail(string email)
        {
    
            var entity = await _DbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with email {email} not found.");
            }
            return entity;

        }



        //public async Task<T> GetByEmail(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException(nameof(entity));
        //    }
        //    if (string.IsNullOrEmpty(EF.Property<string>(entity, "Email")))
        //    {
        //        throw new ArgumentException("Email property is null or empty.", nameof(entity));
        //    }
        //    return await _DbSet.FindAsync(EF.Property<string>(entity, "Email"));
        //}


    }
}
