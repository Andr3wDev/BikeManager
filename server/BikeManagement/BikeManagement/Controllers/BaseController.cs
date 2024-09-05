using BikeManagement.Helpers;
using BikeManagement.Interfaces.UnitOfWork;
using BikeManagement.Responses;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace BikeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseController(IMapper mapper, ILogger logger, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            Logger = logger;
            UnitOfWork = unitOfWork;
        }

        protected IActionResult ApiResult<T>(bool success, string message, T data, int statusCode)
        {
            var response = new ApiResponse<T>(success, message, data, statusCode);
            return StatusCode(statusCode, response);
        }

        protected IActionResult ApiOk<T>(T data, string message = Constants.RESPONSE_SUCCESS)
        {
            return ApiResult(true, message, data, StatusCodes.Status200OK);
        }

        protected IActionResult ApiCreated<T>(T data, string message = Constants.RESPONSE_CREATED_SUCCESSFULLY)
        {
            return ApiResult(true, message, data, StatusCodes.Status201Created);
        }

        protected IActionResult ApiNoContent(string message = Constants.RESPONSE_NO_CONTENT)
        {
            return ApiResult<object>(true, message, null, StatusCodes.Status204NoContent);
        }

        protected IActionResult ApiBadRequest(List<string> errors)
        {
            return ApiResult<object>(false, string.Join("; ", errors), null, StatusCodes.Status400BadRequest);
        }

        protected IActionResult ApiNotFound(string message = Constants.RESPONSE_RESOURCE_NOT_FOUND)
        {
            return ApiResult<object>(false, message, null, StatusCodes.Status404NotFound);
        }

        protected IActionResult ApiInternalServerError(List<string> errors = null)
        {
            errors ??= [Constants.RESPONSE_INTERNAL_SERVER];
            return ApiResult<object>(false, string.Join("; ", errors), null, StatusCodes.Status500InternalServerError);
        }
    }
}
