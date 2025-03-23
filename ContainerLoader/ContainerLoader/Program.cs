using ContainerLoader;

namespace ContainerLoader;

internal class Program
{
    static void Main(string[] args)
    {
        // 1. Create two ships
        var ship1 = new ContainerShip(30, 5, 60); // speed, max containers, max weight in tons
        var ship2 = new ContainerShip(25, 5, 70);

        // 2. Create containers
        var refrigerated = new RefrigeratedContainer(250, 120, 400, 1000, temperature: 7.5); // 7.5°C
        var liquid = new LiquidContainer(240, 100, 350, 900);
        var gas = new GasContainer(260, 110, 380, 950, pressure: 5.5);

        // 3. Create cargo
        var cheese = new RefrigeratedCargo(RefrigeratedProduct.Cheese, 400);       // allowed (7.2°C)
        var butter = new RefrigeratedCargo(RefrigeratedProduct.Butter, 500);       // ❌ temp too high
        var fuel = new HazardousCargoItem("Fuel", 300, HazardousCargoTypes.Fuel);  // hazardous
        var propane = new OrdinaryCargo("Propane", 250);                            // ordinary gas

        // 4. Load cargo into containers
        refrigerated.LoadContainer(cheese);   // ✅
        refrigerated.LoadContainer(butter);   // ❌ should fail (temp)
        liquid.LoadContainer(fuel);           // ✅ (hazardous, allows up to 50%)
        gas.LoadContainer(propane);           // ✅

        // 5. Load containers onto ship1
        ship1.LoadContainer(refrigerated);
        ship1.LoadContainer(liquid);
        ship1.LoadContainer(gas);

        // 6. Transfer gas container to ship2
        ship1.TransferContainerTo(ship2, gas);

        // 7. Unload gas container (leave 5%)
        gas.EmptyCargo();

        // 8. Print info
        Console.WriteLine("\n🚢 Ship 1:");
        ship1.PrintInfo();

        Console.WriteLine("\n🚢 Ship 2:");
        ship2.PrintInfo();
    }
}