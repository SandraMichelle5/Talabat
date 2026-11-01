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

        Query=spec.Includes.Aggregate(Query, (current, includeExperession) => current.Include(includeExperession));
        
        return Query;
    }
}
