using _1.API.Response;
using _3.Data.Models;
using AutoMapper;

namespace _1.API.Mapper;

public class ModelToResponse: Profile
{
    public ModelToResponse()
    {
        CreateMap<Customer, CustomerResponse>();
        CreateMap<Product, ProductResponse>(); 
        CreateMap<Artisan, ArtisanResponse>();
        CreateMap<Order, OrderResponse>();
        CreateMap<Personalization, PersonalizationResponse>();
    }
}