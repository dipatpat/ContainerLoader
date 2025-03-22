namespace ContainerLoader;

public abstract class Container
{
    public List<Cargo> Contents { get; } = new();
    // Cargo mass = weight of cargo only (without container)
    public double CargoMass => Contents.Sum(c => c.Weight);
    public double Height { get; protected set; }
    public double Depth { get; protected set; }

    // The weight of the container itself (for single cargo use)
    public double TareWeight { get; protected set; }
    
    // The weight of the cargo itself (for single cargo use)

    // Total weight (cargo + container)
    public double CargoWeight => CargoMass + TareWeight;

    public double MaxPayload { get; protected set; }

    public string SerialNumber { get; protected set; }
    
    protected Container(
        double height,
        double depth,
        double tareWeight,
        double maxPayload,
        string typeCode)
    {
        Height = height;
        Depth = depth;
        TareWeight = tareWeight;
        MaxPayload = maxPayload;
        SerialNumber = GenerateSerialNumber(typeCode, ContainerIdGenerator.GetNextId());
    }
    
    private string GenerateSerialNumber(string typeCode, int uniqueId)
    {
        return $"KON-{typeCode}-{uniqueId}";
    }

    public virtual void EmptyCargo()
    {
        Contents.Clear();
    }

    public virtual void LoadContainer(Cargo cargo)
    {
        double newCargoMass = CargoMass + cargo.Weight;
        if (newCargoMass > MaxPayload)
        {
            throw new OverfillException(CargoMass, MaxPayload);
        }
        else
        {
            Contents.Add(cargo);
        }
    }
    
    
    

}