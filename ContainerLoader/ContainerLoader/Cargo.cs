namespace ContainerLoader;

public abstract class Cargo(string name, double weight)
{
    public string Name { get; protected set; } = name;
    public double Weight { get; protected set; } = weight;
    
    public virtual bool IsHazardous => false;
}