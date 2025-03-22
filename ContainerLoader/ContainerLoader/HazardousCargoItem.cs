using System.Runtime.Intrinsics.Arm;

namespace ContainerLoader;

public class HazardousCargoItem(string name, double weight, HazardousCargoTypes type)
    : Cargo(name, weight)
{
    public override HazardousCargoTypes? HazardType => type;
}