using ContainerLoader;

namespace ContainerLoader;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting container simulation...\n");

        // 1. Create two ships
        var ship1 = new ContainerShip(30, 5, 60); // Max 5 containers, 60 tons
        var ship2 = new ContainerShip(25, 5, 70); // Max 5 containers, 70 tons

        // 2. Create containers
        var refrigerated = new RefrigeratedContainer(250, 120, 400, 1000, temperature: 7.5);
        var liquid = new LiquidContainer(240, 100, 350, 900);
        var gas = new GasContainer(260, 110, 380, 950, pressure: 5.5);

        // 3. Create cargo
        var cheese = new RefrigeratedCargo(RefrigeratedProduct.Cheese, 400);     // pass
        var butter = new RefrigeratedCargo(RefrigeratedProduct.Butter, 500);     // fail requires 20.5°C
        var iceCream = new RefrigeratedCargo(RefrigeratedProduct.IceCream, 600); // fail requires -18°C

        var fuel = new HazardousCargoItem("Fuel", 100, HazardousCargoTypes.Fuel); // pass
        var water = new OrdinaryCargo("Water", 600); // fail - triggers OverfillException

        var propane = new GasCargo("Propane", 250);  // pass
        var methane = new GasCargo("Methane", 150);  // pass

        // 4. Load refrigerated cargo
        Console.WriteLine("Loading refrigerated cargo...");
        refrigerated.LoadContainer(cheese);   // pass
        refrigerated.LoadContainer(butter);   // fail, temp too high
        refrigerated.LoadContainer(iceCream); // fail, temp too low

        // 5. Load liquid cargo
        Console.WriteLine("\n Loading liquid cargo...");
        try
        {
            liquid.LoadContainer(fuel); // fail, Hazardous cargo exceeds 50%
        }
        catch (OverfillException e)
        {
            Console.WriteLine($"Caught OverfillException: {e.Message}");
        }

        try
        {
            liquid.LoadContainer(water); // pass, Ordinary cargo
        }
        catch (OverfillException e)
        {
            Console.WriteLine($"Unexpected OverfillException: {e.Message}");
        }

        // 6. Load gas cargo
        Console.WriteLine("\nLoading gas cargo...");
        try
        {
            gas.LoadContainer(propane);  // pass
            gas.LoadContainer(methane);  // pass
        }
        catch (OverfillException e)
        {
            Console.WriteLine($"Unexpected OverfillException (gas): {e.Message}");
        }

        // 7. Load containers onto ship1
        ship1.LoadContainer(refrigerated);
        ship1.LoadContainer(liquid);
        ship1.LoadContainer(gas);

        // 8. Transfer gas container to ship2
        Console.WriteLine("\nTransferring gas container to Ship 2...");
        ship1.TransferContainerTo(ship2, gas);

        // 9. Empty gas container (leave 5% mixed gas residue)
        Console.WriteLine("\n Emptying gas container...");
        gas.EmptyCargo();

        // 10. Print final state of both ships
        Console.WriteLine("\nFinal state — Ship 1:");
        PrintShipWithContents(ship1);

        Console.WriteLine("\nFinal state — Ship 2:");
        PrintShipWithContents(ship2);
        
        // Additional Ship Operation Tests
        Console.WriteLine("\n Running ship operations tests...");

        var tinyShip = new ContainerShip(maxSpeed: 20, maxNumberOfAllowedContainers: 2, maxContainersWeight: 1); // 1 ton max
        var backupShip = new ContainerShip(22, 2, 2);

        var small1 = new LiquidContainer(200, 100, 300, 800);
        var small2 = new LiquidContainer(200, 100, 300, 800);
        var small3 = new LiquidContainer(200, 100, 300, 800);

        try
        {
            small1.LoadContainer(new OrdinaryCargo("Water", 400)); // 700 kg
            small2.LoadContainer(new OrdinaryCargo("Juice", 450)); // 750 kg
            small3.LoadContainer(new OrdinaryCargo("Milk", 500));  // 800 kg
            Console.WriteLine("Test: Loaded cargo into all small containers");
        }
        catch
        {
            Console.WriteLine("Unexpected error while loading small containers");
        }

        tinyShip.LoadContainer(small1);
        tinyShip.LoadContainer(small2);

        bool loaded = tinyShip.LoadContainer(small3);
        Console.WriteLine(loaded
            ? "Test: Should NOT allow loading third container (over limit)"
            : "Test: Prevented loading third container (over limit)");

        var tooHeavy = new LiquidContainer(200, 100, 300, 2000);
        tooHeavy.LoadContainer(new OrdinaryCargo("Heavy Water", 800)); // 1100 kg
        bool overWeight = tinyShip.LoadContainer(tooHeavy);
        Console.WriteLine(overWeight
            ? "Test: Should NOT allow overweight container"
            : "Test: Prevented overweight container");

        tinyShip.ReplaceContainer(small2, small3);
        Console.WriteLine("Test: Replaced container on ship");

        bool transferred = tinyShip.TransferContainerTo(backupShip, small1);
        Console.WriteLine(transferred
            ? "Test: Successfully transferred container"
            : "Test: Transfer failed unexpectedly");

        tinyShip.RemoveContainer(small3);
        Console.WriteLine("Test: Removed container from ship");

        Console.WriteLine("\nShip: tinyShip");
        PrintShipWithContents(tinyShip);
        Console.WriteLine("\nShip: backupShip");
        PrintShipWithContents(backupShip);
        
        Console.WriteLine("\nRunning hazard interface checks...");

        void NotifyViaInterface(ContainerShip ship)
        {
            foreach (var container in ship.Containers)
            {
                if (container is IHazardNotifier notifier)
                {
                    Console.WriteLine($"Calling SendNotification via interface for container {container.SerialNumber}...");
                    notifier.SendNotification(container.SerialNumber);
                }
            }
        }

        NotifyViaInterface(ship1);
        NotifyViaInterface(ship2);
        NotifyViaInterface(tinyShip);
        NotifyViaInterface(backupShip);
    }

    static void PrintShipWithContents(ContainerShip ship)
    {
        ship.PrintInfo();

        foreach (var container in ship.Containers)
        {
            Console.WriteLine($"  {container.SerialNumber} ({container.CargoWeight} kg):");

            foreach (var cargo in container.Contents)
            {
                string hazardFlag = cargo.IsHazardous ? " hazardous " : " not hazardous ";
                Console.WriteLine($"     • {cargo.Name} — {cargo.Weight} kg [{cargo.GetType().Name}]{hazardFlag}");
            }
        }
    }
    
    
}
