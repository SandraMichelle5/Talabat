using Talabat.Core.Models;

namespace Talabat.APIs.DTOs;

public record ProductToReturnDTO
(

     string Name,
     string Description ,
     decimal Price ,
     string PictureUrl ,
     string ProductType ,
     string ProductBrand 
);

