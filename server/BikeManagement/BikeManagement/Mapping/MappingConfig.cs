using BikeManagement.Dtos;
using BikeManagement.Models;
using Mapster;

namespace BikeManagement.Mapping
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Bike, BikeDto>
                .NewConfig();

            TypeAdapterConfig<BikeCreateDto, Bike>
                .NewConfig();

            TypeAdapterConfig<BikeUpdateDto, Bike>
                .NewConfig();
        }
    }
}
