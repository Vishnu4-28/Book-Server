using E_commerce.Server.Model.Entities;

namespace E_commerce.Server.DAL.BASE
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        Task<T> GetByEmail(string email);

        //Task GetByEmail(T entity);

    }
}
