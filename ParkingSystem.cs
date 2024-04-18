namespace ParkingLotConsoleApp;

public class ParkingSystem
{
    private readonly ParkingLot parkingLot;

    public ParkingSystem(int capacity)
    {
        parkingLot = new ParkingLot(capacity);
    }

    public string ProcessCommand(string command)
    {
        string[] parts = command.Split(' ');
        string action = parts[0].ToLower();

        switch (action)
        {
            case "registration_numbers_for_vehicles_with_odd_plate":
                return GenerateRegistrationNumbersReport(true);
            case "registration_numbers_for_vehicles_with_even_plate":
                return GenerateRegistrationNumbersReport(false);
            case "create_parking_lot":
                int capacity = int.Parse(parts[1]);
                return $"Created a parking lot with {capacity} slots";
            case "park":
                string regNumber = parts[1];
                string color = parts[2];
                VehicleType type = parts[3].ToLower() == "mobil" ? VehicleType.Car : VehicleType.Motorcycle;
                int slotNumber = parkingLot.CheckIn(new Vehicle(regNumber, color, type));
                if (slotNumber != -1)
                {
                    return $"Allocated slot number: {slotNumber}";
                }
                else
                {
                    return "Sorry, parking lot is full";
                }

            case "leave":
                int slotToLeave = int.Parse(parts[1]);
                if (parkingLot.CheckOut(slotToLeave))
                {
                    return $"Slot number {slotToLeave} is free";
                }
                else
                {
                    return $"Slot number {slotToLeave} is already empty";
                }
            case "status":
                return GenerateStatusReport();
            case "type_of_vehicles":
                VehicleType vehicleType = parts[1].ToLower() == "mobil" ? VehicleType.Car : VehicleType.Motorcycle;
                return parkingLot.GetSlotsByVehicleType(vehicleType).ToString();
            case "registration_numbers_for_vehicles_with_colour":
                string colorToSearch = parts[1];
                return string.Join(", ", parkingLot.GetRegistrationNumbersByVehicleColor(colorToSearch));
            case "slot_numbers_for_vehicles_with_colour":
                string colorForSlots = parts[1];
                return GenerateSlotNumbersByColorReport(colorForSlots);
            case "slot_number_for_registration_number":
                string regNumberToSearch = parts[1];
                int slotNumberForReg = parkingLot.GetSlotNumberByRegistrationNumber(regNumberToSearch);
                return slotNumberForReg != -1 ? slotNumberForReg.ToString() : "Not found";
            default:
                return "Invalid command";
        }
    }

    private string GenerateStatusReport()
    {
        var report = "Slot\tNo.\tType\tRegistration No\tColour\n";
        foreach (var kvp in parkingLot)
        {
            var vehicle = kvp.Value;
            string type = vehicle.Type == VehicleType.Car ? "Mobil" : "Motor";
            string colour = vehicle.Color;

            if (colour.Equals("Putih", StringComparison.OrdinalIgnoreCase))
            {
                colour = "Putih";
            }
            else if (colour.Equals("Hitam", StringComparison.OrdinalIgnoreCase))
            {
                colour = "Hitam";
            }
            else if (colour.Equals("Merah", StringComparison.OrdinalIgnoreCase))
            {
                colour = "Merah";
            }
            else if (colour.Equals("Biru", StringComparison.OrdinalIgnoreCase))
            {
                colour = "Biru";
            }

            report += $"{kvp.Key}\t{vehicle.RegistrationNumber}\t{type.PadRight(10)}\t{colour}\n";
        }
        return report;
    }

    private string GenerateRegistrationNumbersReport(bool isSimilar)
    {
        var report = new List<string>();
        var checkedPlates = new HashSet<string>();

        foreach (var kvp in parkingLot)
        {
            string registrationNumber = kvp.Value.RegistrationNumber;
            string frontPlate = registrationNumber.Split('-')[0];

            // Jika mencari nomor plat yang mirip
            if (isSimilar)
            {
                if (!checkedPlates.Contains(frontPlate))
                {
                    report.Add(registrationNumber);
                    checkedPlates.Add(frontPlate);
                }
            }
            // Jika mencari nomor plat yang unik
            else
            {
                int lastDigit = registrationNumber.Last() - '0';
                if ((lastDigit % 2 == 1 && isSimilar) || (lastDigit % 2 == 0 && !isSimilar))
                {
                    report.Add(registrationNumber);
                }
            }
        }
        return string.Join(", ", report);
    }

    private string GenerateSlotNumbersByColorReport(string color)
    {
        var report = "";
        foreach (var kvp in parkingLot)
        {
            if (kvp.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            {
                report += kvp.Key + ", ";
            }
        }
        return report.TrimEnd(',', ' ');
    }
}
