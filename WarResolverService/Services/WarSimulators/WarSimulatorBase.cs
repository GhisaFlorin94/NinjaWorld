using WarResolverClient.Models;
using WarResolverClient.Services.Interfaces;

namespace WarResolverClient.Services.WarSimulators
{
    internal abstract class WarSimulatorBase : IWarSimulator
    {
        protected static int CalculateTotalPower(IEnumerable<Ninja> ninjas)
        {
            var random = new Random();
            var power = ninjas.Sum(ninja => ninja.Power + ninja.Tools.Sum(tool => tool.Power));
            var luckFactor = random.NextDouble() * 0.4 - 0.2;
            return (int)Math.Round(power + power * luckFactor);
        }

        protected static List<Ninja> CalculateCasualties(List<Ninja> army, int opposingPower, bool isLosingArmy)
        {
            var remainingArmy = new List<Ninja>();
            int powerLoss = isLosingArmy ? (int)(opposingPower * 1.5) : opposingPower;

            foreach (var ninja in army.OrderByDescending(n => n.Power + n.Tools.Sum(t => t.Power)))
            {
                if (powerLoss <= 0)
                {
                    remainingArmy.Add(ninja);
                    continue;
                }

                int ninjaTotalPower = ninja.Power + ninja.Tools.Sum(t => t.Power);
                powerLoss -= ninjaTotalPower;
            }

            return army.Where(a => !remainingArmy.Select(ra => ra.Id).Contains(a.Id)).ToList();
        }

        protected static WarResult CalculateFightOutcome(List<Ninja> attackingList, int attackingPower, List<Ninja> defendingList, int defendingPower)
        {
            var result = new WarResult();
            if (attackingPower > defendingPower)
            {
                result.Winner = attackingList.First().Village;
                result.Loser = defendingList.First().Village;

                result.AttackingArmyCasualties = CalculateCasualties(attackingList, defendingPower, false);
                result.DefendingArmyCasualties = CalculateCasualties(defendingList, attackingPower, true);
            }
            else
            {
                result.Winner = defendingList.First().Village;
                result.Loser = attackingList.First().Village;

                result.DefendingArmyCasualties = CalculateCasualties(defendingList, attackingPower, false);
                result.AttackingArmyCasualties = CalculateCasualties(attackingList, defendingPower, true);
            }

            return result;
        }

        protected abstract int ApplyBattleGroundDefendingFactor(List<Ninja> defendingArmy, int initialPower);

        public abstract WarResult SimulateWar(IEnumerable<Ninja> attackingForce, IEnumerable<Ninja> defendingForce);
    }
}