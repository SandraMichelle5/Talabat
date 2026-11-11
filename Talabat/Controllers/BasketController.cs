using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BasketController : APIBaseController
{
    private readonly IBasketRepository _basketRepo;

    public BasketController(IBasketRepository Repo)
    {
        _basketRepo = Repo;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string Id)
    {
        var basket= await _basketRepo.GetBasketAsync(Id);
        if (basket is null) return new CustomerBasket(Id);
        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>>UpdateBasketAsync(CustomerBasket basket)
    {
        var UpdatedOrCreated = await _basketRepo.UpdateBasketAsync(basket);
        if(UpdatedOrCreated is null)return BadRequest(new ApiResponse(400));
        return Ok(UpdatedOrCreated);
    }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            if (basket == null)
                return NotFound(new ApiResponse(404)); 

            var deleted = await _basketRepo.DeleteBasketAsync(id);
            if (!deleted)
                return StatusCode(500, new ApiResponse(500, "A problem occurred while deleting the basket"));
            return NoContent();
        }

}
