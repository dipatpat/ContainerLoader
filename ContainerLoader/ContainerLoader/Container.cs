namespace ContainerLoader;

public abstract class Container
{
    public double CargoMass { get; protected set; }

    public double Height { get; protected set; }
    public double Depth { get; protected set; }

    public double TareWeight { get; protected set; }

    public double CargoWeight { get; protected set; }

    public double MaxPayload { get; protected set; }

    public string SerialNumber { get; protected set; }
    
    protected Container(
        double cargoMass,
        double height,
        double depth,
        double tareWeight,
        double cargoWeight,
        double maxPayload,
        string typeCode)
    {
        CargoMass = cargoMass;
        Height = height;
        Depth = depth;
        TareWeight = tareWeight;
        CargoWeight = cargoWeight;
        MaxPayload = maxPayload;
        SerialNumber = GenerateSerialNumber(typeCode, ContainerIdGenerator.GetNextId());
    }
    
    private string GenerateSerialNumber(string typeCode, int uniqueId)
    {
        return $"KON-{typeCode}-{uniqueId}";
    }

}