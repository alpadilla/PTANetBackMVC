using Alicunde.System.Exam.EntityFrameworkCore.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Alicunde.System.Exam.EntityFrameworkCore.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        AlicundeSystemExamDbContext Context();
        DbSet<T> Query();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity, Guid id);
        Task DeleteAsync(Guid id);
        Task Clear();
    }

}