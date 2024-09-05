using BikeManagement.Dtos;
using FluentValidation;

namespace BikeManagement.Validation.Bike
{
    public class BikeCreateDtoValidator : AbstractValidator<BikeCreateDto>
    {
        public BikeCreateDtoValidator()
        {
            this.ApplyCommonRules();
        }
    }
}
