﻿// Using MediatR instead

//using System;
//using FluentValidation;
//using Restaurants.Application.Restaurants.Dtos;

//namespace Restaurants.Application.Restaurants.Validators;

//public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
//{
//    private readonly List<string> validCategories = new List<string> { "Italian", "Mexican", "Japanese", "American", "Indian" };

//    public CreateRestaurantDtoValidator()
//    {
//        RuleFor(dto => dto.Name)
//            .Length(3, 100);

//        RuleFor(dto => dto.Description)
//            .NotEmpty().WithMessage("Description is required");

//        RuleFor(dto => dto.Category)
//            .Must(category => validCategories.Contains(category))
//            .WithMessage($"Invalid category. Please choose from the valid categories - {string.Join(",", validCategories)}");
//        //.Custom((value, context) =>
//        //{
//        //    var isValidCategory = validCategories.Contains(value);

//        //    if (!isValidCategory)
//        //    {
//        //        context.AddFailure("Category", $"Invalid category. Please choose from the valid categories - {string.Join(",", validCategories)}");

//        //    }
//        //});


//        RuleFor(dto => dto.ContactEmail)
//            .EmailAddress().WithMessage("Valid Email is required");

//        RuleFor(dto => dto.PostalCode)
//            .Matches(@"^\d{2}-\d{3}$").WithMessage("Plese provide a valid postal code (XX-XXX).");
//    }
//}

