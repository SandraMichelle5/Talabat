using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;
public class ProductWithBrandAndTypeSpec : BaseSpecification<Product>
{
    public ProductWithBrandAndTypeSpec(ProductSpecParam param) : 
                                      base (p => (string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search.ToLower())) && ((!param.typeId.HasValue || p.ProductType.Id== param.typeId)
                                      && (!param.brandId.HasValue || p.ProductBrand.Id == param.brandId)))  
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
        if (!string.IsNullOrEmpty(param.sort))
        {
            switch (param.sort)
            {
                case "PriceAsc":
                    SetOrderBy(p => p.Price);
                    break;
                    case "PriceDesc":
                    SetOrderByDescending(p => p.Price);
                    break;
                    case "NameDesc":
                    SetOrderByDescending(p => p.Name);
                    break;
                default:
                    SetOrderBy(p=> p.Name);
                    break;
            }
        }
        ApplyPaging(param.pageSize * (param.pageIndex - 1), param.pageSize);
    }
    public ProductWithBrandAndTypeSpec(int id) : base(p => p.Id == id)
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
    }
}
