namespace ParkingLotConsoleApp;

public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public VehicleType Type { get; set; }

    public Vehicle(string registrationNumber, string color, VehicleType type)
    {
        RegistrationNumber = registrationNumber;
        Color = color;
        Type = type;
    }
}
