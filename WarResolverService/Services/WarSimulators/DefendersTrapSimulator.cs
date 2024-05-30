using WarResolverClient.Models;

namespace WarResolverClient.Services.WarSimulators
{
    internal class DefendersTrapSimulator : WarSimulatorBase
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
            //The attacking side felt into the defenders trap
            var trapFactor = 30;
            return initialPower + defendingArmy.Count * trapFactor;
        }
    }
}
