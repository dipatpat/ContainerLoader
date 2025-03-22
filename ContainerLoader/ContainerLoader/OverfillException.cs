namespace ContainerLoader;

public class OverfillException(double actualWeight, double maxWeight)
    : Exception($"Container is overfilled: {actualWeight} kg used, but max is {maxWeight} kg.")
{
 public double ActualWeight { get; } = actualWeight;
 public double MaxWeight { get; } = maxWeight;
}