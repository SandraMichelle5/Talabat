using Talabat.Core.Repositories;
using Talabat.Core.Models;
using Talabat.Repository.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Specification;

namespace Talabat.Repository;
public class GenericRepository<T> : IGenericRepositories<T> where T : BaseEntity
{
    private readonly dbContext _dbContext;

    public GenericRepository(dbContext context )
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbContext.Set<T>().ToListAsync();
    public async Task<T> GetById(int id)
        => await _dbContext.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAllWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();

    }
    public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }
}

