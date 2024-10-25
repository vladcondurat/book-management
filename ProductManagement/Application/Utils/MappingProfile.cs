using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
    }
}