using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositories;
public interface IGenericRepositories<T> where T : BaseEntity
{

	public Task<IReadOnlyList<T>> GetAllAsync();
	public Task<T> GetById(int id); 
	public Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
	public Task<T> GetByIdWithSpec(ISpecification<T> spec);
    public Task<int> CountAsync(ISpecification<T> spec);

}
