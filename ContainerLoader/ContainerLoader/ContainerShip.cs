namespace ContainerLoader;

public class ContainerShip
{
    private readonly List<Container> containers = new();

    public IReadOnlyList<Container> Containers => containers;

    private readonly double maxSpeed; // in knots
    private readonly int maxNumberOfAllowedContainers;
    private readonly double maxContainersWeight; // in tons

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
        Console.WriteLine($"Loaded container {container.SerialNumber} onto ship.");
        return true;
    }

    // Load multiple containers at once
    public bool LoadListOfContainers(List<Container> newContainers)
    {
        foreach (var container in newContainers)
        {
            if (!LoadContainer(container)) return false;
        }

        return true;
    }

    // Remove a container from the ship
    public void RemoveContainer(Container container)
    {
        containers.Remove(container);
    }

    // Replace one container with another
    public void ReplaceContainer(Container oldContainer, Container newContainer)
    {
        RemoveContainer(oldContainer);
        LoadContainer(newContainer);
    }

    public void PrintInfo()
    {
        Console.WriteLine("Ship Information:");
        Console.WriteLine($"Max Speed: {maxSpeed} knots");
        Console.WriteLine($"Max Containers: {maxNumberOfAllowedContainers}");
        Console.WriteLine($"Max Weight: {maxContainersWeight} tons");
        Console.WriteLine($"Current Load: {containers.Count} containers");

        double totalWeight = containers.Sum(c => c.CargoWeight);
        Console.WriteLine($"Current Weight: {totalWeight / 1000.0:F2} tons");

        foreach (var container in containers)
        {
            Console.WriteLine($"- {container.SerialNumber} ({container.CargoWeight} kg)");
        }
    }

    // Transfer container from this ship to another
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
    
    public void NotifyHazardousContainers()
    {
        foreach (var container in containers)
        {
            if (container is IHazardNotifier notifier)
            {
                notifier.SendNotification(container.SerialNumber);
            }
        }
    }
}
