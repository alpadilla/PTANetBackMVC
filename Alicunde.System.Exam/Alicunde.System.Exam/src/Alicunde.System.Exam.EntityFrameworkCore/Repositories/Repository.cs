using Alicunde.System.Exam.Domain;
using Alicunde.System.Exam.EntityFrameworkCore.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Alicunde.System.Exam.EntityFrameworkCore.Repositories;

public class Repository<T> : IRepository<T> where T : BaseTrackableEntity, new()
{
    private readonly AlicundeSystemExamDbContext _alicundeSystemExamDbContext;

    public Repository(AlicundeSystemExamDbContext alicundeSystemExamDbContext)
    {
        _alicundeSystemExamDbContext = alicundeSystemExamDbContext;
    }

    public AlicundeSystemExamDbContext Context()
    {
        return _alicundeSystemExamDbContext;
    }

    public DbSet<T> Query()
    {
        return _alicundeSystemExamDbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        try
        {
            return await _alicundeSystemExamDbContext.Set<T>()
                .Where(entity => entity.DeleteAt == null)
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        }
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await _alicundeSystemExamDbContext.Set<T>()
                .FirstOrDefaultAsync(entity => 
                    entity.Id == id &&
                    entity.DeleteAt == null
                );
            
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity {nameof(T)} was not found.");
            }
            _alicundeSystemExamDbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        catch (Exception)
        {
            throw new Exception($"{typeof(T).Name} could not retrieved");
        }
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(entity)} should not be null");
        try
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            await _alicundeSystemExamDbContext.AddAsync(entity);
            await _alicundeSystemExamDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"{typeof(T).Name} could not be saved");
        }
    }

    public async Task<T> UpdateAsync(T entity, Guid id)
    {
        try
        {
            var ent = await GetByIdAsync(id);
            entity.Id = ent.Id;
            entity.UpdatedAt = DateTime.UtcNow;
            _alicundeSystemExamDbContext.Update(entity);
            await _alicundeSystemExamDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"{typeof(T).Name} could not be updated");
        }
    }
    
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            entity.DeleteAt = DateTime.UtcNow;
            _alicundeSystemExamDbContext.Update(entity);
            await _alicundeSystemExamDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception($"{typeof(T).Name} could not be deleted");
        }
    }

    public async Task Clear()
    {
        await _alicundeSystemExamDbContext.Database.ExecuteSqlRawAsync($"DELETE FROM dbo.{typeof(T).Name}");
    }
}
