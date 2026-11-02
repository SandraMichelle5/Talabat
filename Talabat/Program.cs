using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Core.Models;
using Talabat.APIs.Helper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<dbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddApplicationServices();

var app = builder.Build();

using var scoped=app.Services.CreateScope();
var services = scoped.ServiceProvider;
try
{

    var DbContext = services.GetRequiredService<dbContext>();

    await DbContext.Database.MigrateAsync();
    await dbContextSeed.SeedAsync(DbContext);

}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

#region MiddelWare
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion