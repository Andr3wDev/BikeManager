using BikeManagement.Interfaces;
using FluentValidation;

namespace BikeManagement.Validation.Bike
{
    public static class BikeCommonValidationRules
    {
        public static void ApplyCommonRules<T>(this AbstractValidator<T> validator) where T : IBikeDto
        {
            validator.RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Brand is required.");

            validator.RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is required.");

            validator.RuleFor(x => x.Year)
                .GreaterThan(0)
                .WithMessage("Year must be greater than 1900.");

            validator.RuleFor(x => x.OwnerEmail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Valid email is required.");
        }
    }
}
