using WarResolverClient.Models;
using WarResolverClient.Services.Interfaces;

namespace WarResolverClient.Services
{
    internal class BattleAgregatorService : IBattleAgregatorService
    {
        public WarResult AgregateWarResults(List<WarResult> fightResults)
        {
            //Logic to agregate the results
            return fightResults.First();
        }
    }
}
