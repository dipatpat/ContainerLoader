# ContainerLoader Simulation

A C# console simulation of a modern shipping system that models different types of containers, cargo, and container ships — complete with safety checks for hazardous materials.

---

## Features

- Support for various cargo types:
  - Ordinary (e.g. Water, Milk)
  - Hazardous (e.g. Fuel, Explosives)
  - Refrigerated (e.g. Ice Cream, Cheese)
  - Gas-based (e.g. Propane, Methane)
- Container types:
  - `LiquidContainer`
  - `GasContainer`
  - `RefrigeratedContainer`
- Intelligent weight and hazard checks
- Use of `IHazardNotifier` interface for **polymorphic hazard notification**
- Simulated loading, overfill protection, and inter-ship transfers
- Clean test output for real-world scenarios

---

## Example Use Cases

- Prevent overloading of hazardous liquid containers
- Trigger hazard alerts when dangerous cargo is detected
- Reject refrigerated cargo that doesn't meet temperature rules
- Transfer containers between ships

---

## ▶️ Running the Project

### Prerequisites
- [.NET SDK 6.0 or later](https://dotnet.microsoft.com/download)


