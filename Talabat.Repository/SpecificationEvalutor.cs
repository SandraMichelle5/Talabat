using Microsoft.EntityFrameworkCore;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Repository;
public static class SpecificationEvalutor<T> where T : BaseEntity
{

    public static IQueryable<T>GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var Query =inputQuery;

       if(spec.Criteria != null)
        {
            Query = Query.Where(spec.Criteria);
        }

       if(spec.OrderBy != null)
        {
            Query = Query.OrderBy(spec.OrderBy);
        }
       if(spec.OrderByDescending != null)
        {
            Query = Query.OrderByDescending(spec.OrderByDescending);
        }
        Query =spec.Includes.Aggregate(Query, (current, includeExperession) => current.Include(includeExperession));
        
        return Query;
    }
}
