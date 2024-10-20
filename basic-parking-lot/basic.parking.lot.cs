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



