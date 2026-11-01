using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification;
public class ProductWithBrandAndTypeSpec : BaseSpecification<Product>
{
    public ProductWithBrandAndTypeSpec() : base()
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
    }
    public ProductWithBrandAndTypeSpec(int id) : base(p => p.Id == id)
    {
        Includes.Add(p => p.ProductBrand);
        Includes.Add(p => p.ProductType);
    }
}
