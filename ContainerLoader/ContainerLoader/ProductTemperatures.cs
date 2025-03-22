namespace ContainerLoader;

public class ProductTemperatures
{
        public static readonly Dictionary<RefrigeratedProduct, double> TemperatureByProduct =
            new()
            {
                { RefrigeratedProduct.Bananas, 13.3 },
                { RefrigeratedProduct.Chocolate, 18 },
                { RefrigeratedProduct.Fish, 2 },
                { RefrigeratedProduct.Meat, -15 },
                { RefrigeratedProduct.IceCream, -18 },
                { RefrigeratedProduct.FrozenPizza, -30 },
                { RefrigeratedProduct.Cheese, 7.2 },
                { RefrigeratedProduct.Sausages, 5 },
                { RefrigeratedProduct.Butter, 20.5 },
                { RefrigeratedProduct.Eggs, 19 }
            };
    }

