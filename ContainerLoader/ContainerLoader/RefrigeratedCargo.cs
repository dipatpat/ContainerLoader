namespace ContainerLoader;

public class RefrigeratedCargo : Cargo
{
    public RefrigeratedProduct ProductType { get;  }

    public RefrigeratedCargo(RefrigeratedProduct productType, double weight)
        : base(productType.ToString(), weight)
    {
        ProductType = productType;
    }
    public double RequiredTemperature => ProductTemperatures.TemperatureByProduct[ProductType];
}