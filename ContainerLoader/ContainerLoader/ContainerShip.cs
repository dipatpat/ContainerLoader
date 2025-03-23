namespace ContainerLoader;

public class ContainerShip
{
    private List<Container> containers = new();
    private readonly double maxSpeed; //in knots 
    private readonly int maxNumberOfAllowedContainers;
    private readonly double maxContainersWeight; //in tons

    public ContainerShip(double maxSpeed, int maxNumberOfAllowedContainers, double maxContainersWeight)
    {
        this.maxSpeed = maxSpeed;
        this.maxNumberOfAllowedContainers = maxNumberOfAllowedContainers;
        this.maxContainersWeight = maxContainersWeight;
    }
    
    public bool LoadContainer(Container container)
    {
        if (containers.Count >= maxNumberOfAllowedContainers)
        {
            Console.WriteLine("Ship cannot take more containers — limit reached.");
            return false;
        }

        double totalWeight = containers.Sum(c => c.CargoWeight) + container.CargoWeight;

        if (totalWeight > maxContainersWeight * 1000) // convert tons to kg
        {
            Console.WriteLine("Ship is overweight with this container.");
            return false;
        }

        containers.Add(container);
        return true;
    }

    public bool LoadListOfContainers(List<Container> newContainers)
    {
        foreach (var container in newContainers)
        {
            if (!LoadContainer(container)) return false;
        }

        return true;
    }

    public void RemoveContainer(Container container)
    {
        containers.Remove(container);
    }

    public void ReplaceContainer(Container oldContainer, Container newContainer)
    {
        RemoveContainer(oldContainer);
        LoadContainer(newContainer);
    }
    
    public void PrintInfo()
    {
        Console.WriteLine("📦 Ship Information:");
        Console.WriteLine($"Max Speed: {maxSpeed} knots");
        Console.WriteLine($"Max Containers: {maxNumberOfAllowedContainers}");
        Console.WriteLine($"Max Weight: {maxContainersWeight} tons");
        Console.WriteLine($"Current Load: {containers.Count} containers");

        double totalWeight = containers.Sum(c => c.CargoWeight);
        Console.WriteLine($"Current Weight: {totalWeight / 1000.0:F2} tons");

        foreach (var c in containers)
        {
            Console.WriteLine($"- {c.SerialNumber} ({c.CargoWeight} kg)");
        }
    }
    
    public bool TransferContainerTo(ContainerShip destinationShip, Container container)
    {
        if (!containers.Contains(container))
        {
            Console.WriteLine("This container is not on the ship.");
            return false;
        }

        if (!destinationShip.LoadContainer(container))
        {
            Console.WriteLine("Destination ship cannot accept the container.");
            return false;
        }

        RemoveContainer(container);
        Console.WriteLine($"Transferred container {container.SerialNumber}.");
        return true;
    }
}