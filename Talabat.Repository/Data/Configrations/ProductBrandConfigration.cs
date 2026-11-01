using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Models;
namespace Talabat.Repository.Data.Configrations;
internal class ProductBrandConfigration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(p => p.Name).IsRequired();


    }

}
