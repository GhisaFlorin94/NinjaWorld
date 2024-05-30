using WarResolverClient.Models;

namespace WarResolverClient.Services.Interfaces
{
    internal interface IBattleAgregatorService
    {
        WarResult AgregateWarResults(List<WarResult> fightResults);
    }
}