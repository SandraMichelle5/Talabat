using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Extensions;

public static class ApplicationServiceExtensions
{

    public static void AddApplicationServices(this IServiceCollection Services)
    {
        Services.AddControllers();
        Services.AddEndpointsApiExplorer();
        Services.AddSwaggerGen();
        Services.AddOpenApi();
        Services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepository<>));

        Services.AddAutoMapper(typeof(MappingProfiles));
        Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });

    }
}
