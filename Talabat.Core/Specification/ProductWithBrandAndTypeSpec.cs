using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;
public class ProductWithBrandAndTypeSpec : BaseSpecification<Product>
{
    public ProductWithBrandAndTypeSpec(string? sort,int? typeId,int? brandId) : 
                                      base(p=>(!typeId.HasValue || p.ProductType.Id==typeId )
                                      && (!brandId.HasValue || p.ProductBrand.Id==brandId))
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
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
    }
    public ProductWithBrandAndTypeSpec(int id) : base(p => p.Id == id)
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
    }
}
