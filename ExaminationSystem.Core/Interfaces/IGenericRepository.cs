using System.Linq.Expressions;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<bool> AnyAsync();

        Task<int> SaveAsync();

    }
}
