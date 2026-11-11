using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;
public class ProductWithFiltrationCountAsync : BaseSpecification<Product>
{
    public ProductWithFiltrationCountAsync(ProductSpecParam param) :
                                      base(p => (string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search.ToLower())) && ((!param.typeId.HasValue || p.ProductType.Id == param.typeId)
                                      && (!param.brandId.HasValue || p.ProductBrand.Id == param.brandId)))
    {
    }
}
