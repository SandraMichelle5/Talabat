using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories;
using Talabat.Core.Models;
using Talabat.Core.Specification;
using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers;

public class ProductsController : APIBaseController
{

    private readonly IGenericRepositories<Product> _productRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepositories<Product> ProductRepo, IMapper mapper)
    {
        _productRepo = ProductRepo;
        _mapper = mapper;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts()
    {
        var spec=new ProductWithBrandAndTypeSpec();
        var products = await _productRepo.GetAllWithSpec(spec);
        var productsDto = _mapper.Map< IEnumerable<ProductToReturnDTO>>(products);
        return Ok(productsDto);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int id)
    {
      var spec= new ProductWithBrandAndTypeSpec(id);
        var product = await _productRepo.GetByIdWithSpec(spec);
        if (product is null) return NotFound(new ApiResponse(404));
        var productDto = _mapper.Map<ProductToReturnDTO>(product);
        return Ok(productDto);
    }
}
