using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;


namespace Talabat.Core.Specification;
public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }

    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDescending { get; set; }
    public int Skip { get;  set; }
    public int Take { get;  set; }
    public bool IsPagingEnabled { get;  set; }
    public BaseSpecification()
    {

    }
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public void SetOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }
    public void SetOrderByDescending(Expression<Func<T, object>> orderByDescending)
    {
        OrderByDescending = orderByDescending;
    }
    public void ApplyPaging(int skip, int take)
    {
        IsPagingEnabled= true;
        Skip = skip;
        Take = take;
    }

}
