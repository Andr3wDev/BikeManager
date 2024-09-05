namespace BikeManagement.Interfaces
{
    public interface IBikeDto
    {
        string Brand { get; set; }
        string Model { get; set; }
        int Year { get; set; }
        string OwnerEmail { get; set; }
    }
}
