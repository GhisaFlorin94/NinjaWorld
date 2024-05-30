using WarResolverClient.Models;
using WarResolverClient.Models.Enums;
using WarResolverClient.Services.Interfaces;

namespace WarResolverClient.Services.WarSimulators
{
    internal class InsideVillageSimulator : WarSimulatorBase
    {
        public override WarResult SimulateWar(IEnumerable<Ninja> attackingForce, IEnumerable<Ninja> defendingForce)
        {
            var attackingList = attackingForce.ToList();
            var defendingList = defendingForce.ToList();

            int attackingPower = CalculateTotalPower(attackingList);
            int defendingPower = ApplyBattleGroundDefendingFactor(defendingList, CalculateTotalPower(defendingList));

            var result = CalculateFightOutcome(attackingList, attackingPower, defendingList, defendingPower);

            return result;
        }

        protected override int ApplyBattleGroundDefendingFactor(List<Ninja> defendingArmy, int initialPower)
        {
            //The defending force are distracted by helping civilians.
            var distractionFactor = 0.8;
            return (int)(initialPower - initialPower * distractionFactor);
        }
    }
}
