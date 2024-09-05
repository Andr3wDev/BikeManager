using FluentValidation;
using BikeManagement.Dtos;
using FluentValidation.Results;

namespace BikeManagement.Services
{
    public class BikeValidationService : IBikeValidationService
    {
        private readonly IValidator<BikeCreateDto> _createValidator;
        private readonly IValidator<BikeUpdateDto> _updateValidator;

        public BikeValidationService(
            IValidator<BikeCreateDto> createValidator,
            IValidator<BikeUpdateDto> updateValidator)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public Task<ValidationResult> ValidateCreateAsync(BikeCreateDto createDto)
        {
            return _createValidator.ValidateAsync(createDto);
        }

        public Task<ValidationResult> ValidateUpdateAsync(BikeUpdateDto updateDto)
        {
            return _updateValidator.ValidateAsync(updateDto);
        }
    }
}
