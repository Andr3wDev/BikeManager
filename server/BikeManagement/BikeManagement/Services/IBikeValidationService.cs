namespace BikeManagement.Services
{
    using BikeManagement.Dtos;
    using FluentValidation.Results;

    public interface IBikeValidationService
    {
        Task<ValidationResult> ValidateCreateAsync(BikeCreateDto createDto);
        Task<ValidationResult> ValidateUpdateAsync(BikeUpdateDto updateDto);
    }

}
