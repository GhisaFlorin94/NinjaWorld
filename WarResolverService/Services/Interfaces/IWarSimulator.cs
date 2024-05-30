using WarResolverClient.Models;

namespace WarResolverClient.Services.Interfaces
{
    internal interface IWarSimulator
    {
        WarResult SimulateWar(IEnumerable<Ninja> attackingForce, IEnumerable<Ninja> defendingForce);
    }
}