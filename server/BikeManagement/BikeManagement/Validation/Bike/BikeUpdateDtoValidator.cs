using BikeManagement.Dtos;
using FluentValidation;

namespace BikeManagement.Validation.Bike
{
    public class BikeUpdateDtoValidator : AbstractValidator<BikeUpdateDto>
    {
        public BikeUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

            this.ApplyCommonRules();
        }
    }
}
