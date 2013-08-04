using System;
using FluentValidation;
using ChART.Domain.Entities;

namespace ChARTServices.Models
{
    public class StationValidator : AbstractValidator<Station>
    {
        public StationValidator()
        {
            RuleFor(s => s.Id).NotNull();
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Location).NotNull();
        }
    }
}