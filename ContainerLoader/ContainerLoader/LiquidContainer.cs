namespace ContainerLoader;

public class LiquidContainer(double height, double depth, double tareWeight, double maxPayload)
    : Container(height, depth, tareWeight, maxPayload, "L"), IHazardNotifier
{
    // Determines whether the container would be hazardous if the new cargo is added
    private bool WouldBeHazardousWith(Cargo newItem)
    {
        return newItem.IsHazardous || Contents.Any(c => c.IsHazardous);
    }

    public override void LoadContainer(Cargo item)
    {
        bool willBeHazardous = WouldBeHazardousWith(item);
        double safePayload = willBeHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;

        double newCargoMass = CargoMass + item.Weight;

        if (newCargoMass > safePayload)
        {
            throw new OverfillException(newCargoMass + TareWeight, MaxPayload);
        }

        Contents.Add(item);
        if (item.IsHazardous || Contents.Any(c => c.IsHazardous))
        {
            SendNotification(SerialNumber);
        }
        Console.WriteLine($"Loaded liquid cargo: {item.Name} ({item.Weight} kg) into {SerialNumber}");
    }

    public void SendNotification(string serialNumber)
    {
        Console.WriteLine($"HAZARD WARNING: Liquid container {serialNumber} contains hazardous liquid!");
    }
}