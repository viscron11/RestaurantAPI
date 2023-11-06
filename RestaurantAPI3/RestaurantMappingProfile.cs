using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantAPI3.Entities;
using RestaurantAPI3.Models;

namespace RestaurantAPI3
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, CustomerDto2>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, 
                c => c.MapFrom(dto => new Address()
                    { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
        }
    }
}
