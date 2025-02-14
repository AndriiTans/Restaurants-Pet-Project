﻿using System;
using AutoMapper;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {

            CreateMap<UpdateRestaurantCommand, Restaurant>();

            // Using CreateRestaurantCommand as a part of MediatR instead
            //CreateMap<CreateRestaurantDto, Restaurant>()
            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));


            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.PostalCode, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Street, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));




            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));


            CreateMap<DishDto, Dish>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
