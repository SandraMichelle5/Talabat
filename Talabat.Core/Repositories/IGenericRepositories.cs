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

	#region MyRegion
	public Task<IEnumerable<T>> GetAllAsync();
	public Task<T> GetById(int id); 
	#endregion

	public Task<IEnumerable<T>> GetAllWithSpec(ISpecification<T> spec);
	public Task<T> GetByIdWithSpec(ISpecification<T> spec);

}
