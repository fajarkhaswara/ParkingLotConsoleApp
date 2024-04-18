using System.Collections;

namespace ParkingLotConsoleApp;

public class ParkingLot : IEnumerable<KeyValuePair<int, Vehicle>>
{
    private readonly int capacity;
    private readonly Dictionary<int, Vehicle> parkedVehicles;

    public ParkingLot(int capacity)
    {
        this.capacity = capacity;
        parkedVehicles = new Dictionary<int, Vehicle>();
    }

    public int CheckIn(Vehicle vehicle)
    {
        for (int i = 1; i <= capacity; i++)
        {
            if (!parkedVehicles.ContainsKey(i))
            {
                parkedVehicles[i] = vehicle;
                return i;
            }
        }
        return -1; // Return -1 jika parkiran penuh
    }

    public bool CheckOut(int slotNumber)
    {
        if (parkedVehicles.ContainsKey(slotNumber))
        {
            parkedVehicles.Remove(slotNumber);
            return true;
        }
        return false; // Slot is already empty or doesn't exist
    }

    public int GetTotalOccupiedSlots()
    {
        return parkedVehicles.Count;
    }

    public int GetAvailableSlots()
    {
        return capacity - parkedVehicles.Count;
    }

    public int GetSlotsByVehicleColor(string color)
    {
        return parkedVehicles.Count(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
    }

    public int GetSlotsByVehicleType(VehicleType type)
    {
        return parkedVehicles.Count(v => v.Value.Type == type);
    }

    public List<string> GetRegistrationNumbersByVehicleColor(string color)
    {
        return parkedVehicles.Where(v => v.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                             .Select(v => v.Value.RegistrationNumber)
                             .ToList();
    }

    public int GetSlotNumberByRegistrationNumber(string registrationNumber)
    {
        var vehicle = parkedVehicles.FirstOrDefault(v => v.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        return vehicle.Key != 0 ? vehicle.Key : -1; // Returns -1 if not found
    }

    public IEnumerator<KeyValuePair<int, Vehicle>> GetEnumerator()
    {
        return parkedVehicles.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public string GetRegistrationNumbersByPlateType(bool isOddPlate)
    {
        var registrationNumbers = new List<string>();
        foreach (var kvp in parkedVehicles)
        {
            var registrationNumber = kvp.Value.RegistrationNumber;
            char firstChar = registrationNumber[0];
            bool isOdd = Char.IsLetter(firstChar) && (firstChar - 'A') % 2 == 0; // true for even, false for odd
            if (isOddPlate && isOdd)
            {
                registrationNumbers.Add(registrationNumber);
            }
            else if (!isOddPlate && !isOdd)
            {
                registrationNumbers.Add(registrationNumber);
            }
        }
        return string.Join(", ", registrationNumbers);
    }

}

