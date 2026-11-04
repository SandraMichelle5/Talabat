using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification;
public class ProductSpecParam
{

    public string? sort { get; set; }
    public int? typeId { get; set; }
    public int? brandId { get; set; }
    public int pageIndex { get; set; } = 1;
    
    private int _pageSize = 5;
    public int pageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > 10) ? _pageSize : value; }
    }
}
