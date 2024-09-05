using BikeManagement.Controllers;
using BikeManagement.Dtos;
using BikeManagement.Helpers;
using BikeManagement.Interfaces.UnitOfWork;
using BikeManagement.Models;
using BikeManagement.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BikeController : BaseController
{
    private readonly IBikeValidationService _validationService;

    public BikeController(IMapper mapper, ILogger<BikeController> logger, IUnitOfWork unitOfWork, IBikeValidationService validationService)
        : base(mapper, logger, unitOfWork)
    {
        _validationService = validationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBike([FromBody] BikeCreateDto createDto)
    {
        if (createDto == null)
        {
            return ApiBadRequest(new List<string> { Constants.RESPONSE_NULL_REQUEST });
        }

        var validationResult = await _validationService.ValidateCreateAsync(createDto);
        if (!validationResult.IsValid)
        {
            return ApiBadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var bike = Mapper.Map<Bike>(createDto);
        await UnitOfWork.Bikes.AddAsync(bike);
        await UnitOfWork.SaveChangesAsync();

        Logger.LogInformation("Created new bike with ID {BikeId}", bike.Id);

        var mappedBike = Mapper.Map<BikeDto>(bike);
        return ApiCreated(mappedBike);
    }

    [HttpGet]
    public async Task<IActionResult> GetBikes()
    {
        var bikes = await UnitOfWork.Bikes.GetAllAsync();
        var mappedBikes = Mapper.Map<List<BikeDto>>(bikes);

        return ApiOk(mappedBikes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBike(int id)
    {
        var bike = await UnitOfWork.Bikes.GetByIdAsync(id);
        if (bike == null)
        {
            Logger.LogWarning("Bike with ID {BikeId} not found", id);
            return ApiNotFound($"Bike with id {id} not found");
        }

        var mappedBike = Mapper.Map<BikeDto>(bike);
        return ApiOk(mappedBike);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBike(int id, [FromBody] BikeUpdateDto updateDto)
    {
        if (updateDto == null)
        {
            return ApiBadRequest(new List<string> { Constants.RESPONSE_NULL_REQUEST });
        }

        var validationResult = await _validationService.ValidateUpdateAsync(updateDto);
        if (!validationResult.IsValid)
        {
            return ApiBadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var bike = await UnitOfWork.Bikes.GetByIdAsync(id);
        if (bike == null)
        {
            Logger.LogWarning("Unable to delete bike with ID {BikeId}", id);
            return ApiNotFound($"Bike with id {id} not found");
        }

        Mapper.Map(updateDto, bike);
        UnitOfWork.Bikes.Update(bike);
        await UnitOfWork.SaveChangesAsync();

        Logger.LogInformation("Updated bike with ID {BikeId}", id);

        var mappedBike = Mapper.Map<BikeDto>(bike);
        return ApiOk(mappedBike);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBike(int id)
    {
        var deleted = await UnitOfWork.Bikes.DeleteAsync(id);
        if (!deleted)
        {
            Logger.LogWarning("Unable to delete bike with ID {BikeId}", id);
            return ApiNotFound($"Bike with id {id} not found");
        }

        await UnitOfWork.SaveChangesAsync();

        Logger.LogInformation("Deleted bike with ID {BikeId}", id);
        return ApiOk(new { Message = "Bike deleted successfully." });
    }
}
