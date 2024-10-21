using System;

/// <summary>
/// Enum to define the types of parking spots
/// </summary>
public enum ParkingSpotType
{
    Motorcycle,
    Car,
    Van
}

/// <summary>
/// Base class for a parking spot
/// </summary>
public class ParkingSpot
{
    public ParkingSpotType spotType { get; private set; }
    public bool isOccupied { get; private set; }

    // Constructor
    public ParkingSpot(ParkingSpotType type)
    {
        spotType = type;
        isOccupied = false;
    }

    public void Park()
    {
        if(isOccupied)
            throw new InvalidOperationException("Spot is already occupied.");

        isOccupied = true;
    }

    public void Leave()
    {
        if(!isOccupied)
            throw new InvalidOperationException("Spot is already empty.");

        isOccupied = false;
    }
}

/// <summary>
/// Base class for vehicles
/// </summary>
public abstract class Vehicle
{
    public abstract int spotsNeeded {get;}
    public abstract ParkingSpot spotType {get;}
}

public class Motorcycle : Vehicle
{
    public override int spotsNeeded => 1;
    public override ParkingSpot spotType => ParkingSpotType.Motorcycle;
}

public class Car : Vehicle
{
    public override int spotsNeeded => 1;
    public override ParkingSpot spotType => ParkingSpotType.Car;
}

public class Van : Vehicle
{
    public override int spotsNeeded => 3;
    public override ParkingSpot spotType => ParkingSpotType.Van;
}

/// <summary>
/// Parking lot class to manage parking
/// </summary>
public class ParkingLot
{
    private List<ParkingSpot> motorcycleSpots = new();
    private List<ParkingSpot> carSpots = new();
    private List<ParkingSpot> largeSpots = new();

    public ParkingLot(int motoCount, int carCount, int vanCount)
    {
        for (int i = 0; i < motorcycleCount; i++)
            motorcycleSpots.Add(new ParkingSpot(ParkingSpotType.Motorcycle));
        
        for (int i = 0; i < compactCount; i++)
            carSpots.Add(new ParkingSpot(ParkingSpotType.Car));
        
        for (int i = 0; i < largeCount; i++)
            largeSpots.Add(new ParkingSpot(ParkingSpotType.Van));
    }

    /// <summary>
    /// Park a vehicle in the parking lot
    /// </summary>
    /// <param name="vehicle"></param>
    /// <returns>bool</returns>
    public bool ParkVehicle(Vehicle vehicle)
    {
        switch (vehicle.SpotType)
        {
            case ParkingSpotType.Motorcycle:
                return ParkInAvailableSpot(motorcycleSpots, vehicle.SpotsNeeded);
            case ParkingSpotType.Car:
                return ParkInAvailableSpot(carSpots, vehicle.SpotsNeeded);
            case ParkingSpotType.Van:
                return ParkInAvailableSpot(largeSpots, vehicle.SpotsNeeded);
            default:
                return false;
        }
    }

    /// <summary>
    /// Helper function to park a vehicle in available spots
    /// </summary>
    /// <param name="spots"></param>
    /// <param name="spotsNeeded"></param>
    /// <returns>bool</returns>
    private bool ParkInAvailableSpot(List<ParkingSpot> spots, int spotsNeeded)
    {
        int freeSpots = 0;
        foreach (var spot in spots)
        {
            if (!spot.IsOccupied)
                freeSpots++;
        }

        if (freeSpots >= spotsNeeded)
        {
            for (int i = 0, count = 0; count < spotsNeeded; i++)
            {
                if (!spots[i].IsOccupied)
                {
                    spots[i].Park();
                    count++;
                }
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Tell how many total spots there are
    /// </summary>
    /// <returns></returns>
    public int GetTotalSpots()
    {
        return motorcycleSpots.Count + carSpots.Count + largeSpots.Count;
    }

    /// <summary>
    /// Tell how many spots are remaining
    /// </summary>
    /// <returns></returns>
    public int GetRemainingSpots()
    {
        return GetFreeSpots(motorcycleSpots) + GetFreeSpots(carSpots) + GetFreeSpots(largeSpots);
    }

    /// <summary>
    /// Tell if parking lot is full
    /// </summary>
    /// <returns></returns>
    public bool IsFull()
    {
        return GetRemainingSpots() == 0;
    }

    /// <summary>
    /// Tell if parking lot is empty
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return GetRemainingSpots() == GetTotalSpots();
    }

    /// <summary>
    /// Tell if all motorcycle spots are taken
    /// </summary>
    /// <returns></returns>
    public bool AreMotorcycleSpotsFull()
    {
        return GetFreeSpots(motorcycleSpots) == 0;
    }

    /// <summary>
    /// Tell how many spots vans are taking up
    /// </summary>
    /// <returns></returns>
    public int GetVansParked()
    {
        return (largeSpots.Count - GetFreeSpots(largeSpots)) / 3;
    }

    /// <summary>
    /// Helper method to count free spots in a given list
    /// </summary>
    /// <param name="spots"></param>
    /// <returns></returns>9
    private int GetFreeSpots(List<ParkingSpot> spots)
    {
        int freeSpots = 0;
        foreach (var spot in spots)
        {
            if (!spot.IsOccupied)
                freeSpots++;
        }
        return freeSpots;
    }
}



class Program
{
    static void Main(string[] args)
    {
        // Initialize a parking lot with 5 motorcycle spots, 5 compact spots, and 5 large spots
        ParkingLot parkingLot = new ParkingLot(5, 5, 5);

        // Park a motorcycle
        Vehicle motorcycle = new Motorcycle();
        parkingLot.ParkVehicle(motorcycle);

        // Park a car
        Vehicle car = new Car();
        parkingLot.ParkVehicle(car);

        // Park a van
        Vehicle van = new Van();
        parkingLot.ParkVehicle(van);

        // Display information
        Console.WriteLine("Total spots: " + parkingLot.GetTotalSpots());
        Console.WriteLine("Remaining spots: " + parkingLot.GetRemainingSpots());
        Console.WriteLine("Is full: " + parkingLot.IsFull());
        Console.WriteLine("Is empty: " + parkingLot.IsEmpty());
        Console.WriteLine("Motorcycle spots full: " + parkingLot.AreMotorcycleSpotsFull());
        Console.WriteLine("Vans parked: " + parkingLot.GetVansParked());
    }
}





