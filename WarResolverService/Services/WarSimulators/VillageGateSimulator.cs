using WarResolverClient.Models;
using WarResolverClient.Models.Enums;

namespace WarResolverClient.Services.WarSimulators
{
    internal class VillageGateSimulator : WarSimulatorBase
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
            //the defending force is enhanced by the number of the ninjas, cause they can use the walls
            var wallFactor = 15;
            return initialPower + defendingArmy.Count * wallFactor;
        }
    }
}
