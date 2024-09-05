using BikeManagement.Interfaces;

namespace BikeManagement.Dtos
{
    public class BikeCreateDto : IBikeDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string OwnerEmail { get; set; }
    }
}
