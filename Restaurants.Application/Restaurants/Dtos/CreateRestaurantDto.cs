using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class CreateRestaurantDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; } = default!;

        public string? ContactEmail { get; set; }

        public string? ContactNumber { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }

        public string? PostalCode { get; set; }

        // We are using FluentValidation package instead
        //[StringLength(100, MinimumLength = 3]
        //public string Name { get; set; } = default!;
        //public string Description { get; set; } = default!;

        //[Required(ErrorMessage = "Insert a valid category")]
        //public string Category { get; set; } = default!;
        //public bool HasDelivery { get; set; } = default!;

        //[EmailAddress(ErrorMessage = "Please provide a valid email address")]
        //public string? ContactEmail { get; set; }

        //[Phone(ErrorMessage = "Please provide valid phone number")]
        //public string? ContactNumber { get; set; }

        //public string? City { get; set; }
        //public string? Street { get; set; }

        //[RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Plese provide a valid postal code (XX-XXX).")]
        //public string? PostalCode { get; set; }
    }
}

