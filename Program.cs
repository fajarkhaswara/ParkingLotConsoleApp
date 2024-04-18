namespace ParkingLotConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        ParkingSystem parkingSystem = null;

        while (true)
        {
            string command = Console.ReadLine();
            if (command.ToLower() == "exit")
            {
                break;
            }

            string output;
            if (parkingSystem == null && command.StartsWith("create_parking_lot"))
            {
                // Inisialisasi parkingSystem jika perintah pertama adalah create_parking_lot
                int capacity = int.Parse(command.Split(' ')[1]);
                parkingSystem = new ParkingSystem(capacity);
                output = $"Created a parking lot with {capacity} slots";
            }
            else
            {
                output = parkingSystem != null ? parkingSystem.ProcessCommand(command) : "Parking system has not been initialized";
            }
            Console.WriteLine(output);
        }
    }
}
