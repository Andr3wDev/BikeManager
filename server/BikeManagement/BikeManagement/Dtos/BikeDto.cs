using BikeManagement.Interfaces;

namespace BikeManagement.Dtos
{
    public class BikeDto : IBikeDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string OwnerEmail { get; set; }
    }
}
