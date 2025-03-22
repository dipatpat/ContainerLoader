using System.ComponentModel.Design;

namespace ContainerLoader;

public class RefrigeratedContainer(
    double height,
    double depth,
    double tareWeight,
    double masPayload,
    double temperature)
    : Container(height, depth, tareWeight, masPayload, "C")
{
    public double Temperature { get; set; } = temperature;
    public RefrigeratedProduct? StoredProductType { get; set; }

    private bool IsValidItem(Cargo item)
    {
        if (item is not RefrigeratedCargo coldItem) return false;

        if (Temperature < coldItem.RequiredTemperature) return false;

        StoredProductType ??= coldItem.ProductType;
        return coldItem.ProductType == StoredProductType;
    }

    public override void LoadContainer(Cargo item)
    {
        if (!IsValidItem(item))
        {
            Console.WriteLine("Error: Invalid item, type mismatch or temperature is invalid.");
            return;
        }

        base.LoadContainer(item);
    }
}

