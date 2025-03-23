using System.Runtime.CompilerServices;

namespace ContainerLoader;

public class GasContainer(
    double height,
    double depth,
    double tareWeight,
    double maxPayload,
    double pressure)
    : Container(height, depth, tareWeight, maxPayload, "G"), IHazardNotifier
{
    public double Pressure { get; private set; } = pressure; // in atmospheres 

    public override void LoadContainer(Cargo cargo)
    {
        double newCargoMass = CargoMass + cargo.Weight;
        double newTotalWeight = newCargoMass;

        if (newTotalWeight > MaxPayload)
        {
            throw new OverfillException(newTotalWeight, MaxPayload);
        }

        Contents.Add(cargo);
        if (cargo.IsHazardous || Contents.Any(c => c.IsHazardous))
        {
            SendNotification(SerialNumber);
        }
    }

    public override void EmptyCargo()
    {
        // Leave 5% of each cargo item's weight behind
        double remainingMass = CargoMass * 0.05;

        Contents.Clear();
        // Add a single cargo item representing the remaining mixed gas
        Contents.Add(new OrdinaryCargo("Mixed Gas Residue", remainingMass));
        Console.WriteLine($"Emptied gas container. 5% ({remainingMass}kg) of the mixed gases remain.");
    }

    public void SendNotification(string serialNumber)
    {
        Console.WriteLine($"HAZARD ALERT: Gas container {serialNumber} contains hazardous gas!");
    }
}
    
