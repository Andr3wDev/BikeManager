using BikeManagement.Data.Context;
using BikeManagement.Data.UnitOfWork;
using BikeManagement.Mapping;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BikeManagement.Middleware;
using Serilog;
using BikeManagement.Validation.Bike;
using BikeManagement.Dtos;
using BikeManagement.Services;
using BikeManagement.Interfaces.UnitOfWork;
using Mapster;
using MapsterMapper;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting app");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IValidator<BikeCreateDto>, BikeCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<BikeUpdateDto>, BikeUpdateDtoValidator>();
    builder.Services.AddScoped<IBikeValidationService, BikeValidationService>();

    var config = new TypeAdapterConfig();
    MappingConfig.RegisterMappings();
    builder.Services.AddSingleton(config);
    builder.Services.AddSingleton<IMapper>(new Mapper(config));

    // In-Memory
    builder.Services.AddDbContext<BikeDbContext>(options =>
        options.UseInMemoryDatabase("BikeManagementDB"));

    // CORS for DEV
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowDev", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowDev");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled server exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}