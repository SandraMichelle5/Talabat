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
    private readonly IGenericRepositories<ProductType> _typeRepo;
    private readonly IGenericRepositories<ProductBrand> _brandRepo;

    public ProductsController(IGenericRepositories<Product> ProductRepo, IMapper mapper,IGenericRepositories<ProductType>Typo,IGenericRepositories<ProductBrand>Brand)
    {
        _productRepo = ProductRepo;
        _mapper = mapper;
        _typeRepo = Typo;
        _brandRepo=Brand;
    }

    [HttpGet]

    public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts(string? sort)
    {
        var spec=new ProductWithBrandAndTypeSpec(sort);
        var products = await _productRepo.GetAllWithSpec(spec);
        var productsDto = _mapper.Map< IReadOnlyList<ProductToReturnDTO>>(products);
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

    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>>GetTypes()
    {
        var types = await _typeRepo.GetAllAsync();
        return Ok(types);
    }
    [HttpGet("Brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetBrands()
    {
        var brands = await _brandRepo.GetAllAsync();
        return Ok(brands);
    }

}
