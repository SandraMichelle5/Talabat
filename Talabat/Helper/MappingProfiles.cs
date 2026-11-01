using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Models;

namespace Talabat.APIs.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDTO>()
                   .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                   .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                   .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>()); 
    }

    protected internal MappingProfiles(string profileName) : base(profileName)
    {
    }
}
