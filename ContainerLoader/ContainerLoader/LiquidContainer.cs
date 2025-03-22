using System.Runtime.CompilerServices;

namespace ContainerLoader;

public class LiquidContainer(double height, double depth, double tareWeight, double maxPayload)
    : Container(height, depth, tareWeight, maxPayload, "L"), IHazardNotifier
{
    private bool HasHazardousCargo()
    {
        foreach (var item in Contents)
        {
            if (item.IsHazardous)
                return true;
        }

        return false;
    }

    public double CheckCapacity()
    {
        bool isHazardous = HasHazardousCargo();
        double safePayload = isHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        return safePayload;
    }

    public override void LoadContainer(Cargo item)
    {
        double usableCapacity = CheckCapacity();
        double newCargoMass = CargoMass + item.Weight;

        if (newCargoMass > usableCapacity)
        {
            SendNotification(SerialNumber);
            Console.WriteLine("Attempt to perform a dangerous operation!");
            throw new OverfillException(newCargoMass + TareWeight, MaxPayload);
        }
        Contents.Add(item);
    }

    public void SendNotification(string serialNumber)
    {
        Console.WriteLine($"HAZARD WARNING: Liquid container{serialNumber} exceeded safe limits!");
    }

}