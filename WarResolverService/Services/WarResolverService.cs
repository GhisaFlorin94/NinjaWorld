using WarResolverClient.Models;
using WarResolverClient.Models.Enums;
using WarResolverClient.Services.Interfaces;
using WarResolverClient.Services.WarSimulators;

namespace WarResolverClient.Services
{
    internal class WarResolverService : IWarResolverService
    {
        private static readonly Random _random = new Random();
        private static int _numberOfSimulations = 15;
        private readonly IBattleAgregatorService _battleAgregatorService;

        private readonly Dictionary<BattleGround, IWarSimulator> _battleGroundScenarios;
        public WarResolverService(IBattleAgregatorService battleAgregatorService)
        {
            _battleAgregatorService = battleAgregatorService;

            _battleGroundScenarios = new Dictionary<BattleGround, IWarSimulator>()
            {
                { BattleGround.InsideVillage, new InsideVillageSimulator() },
                { BattleGround.VillageGate, new VillageGateSimulator() },
                { BattleGround.DefendersTrap, new DefendersTrapSimulator() }
            };
        }

        public WarResult ResolveWar(WarDeclarationRequest warDeclarationRequest)
        {      
            var fightResults = new List<WarResult>();

            Parallel.For(0, _numberOfSimulations, index =>
            {
                var battleGround = (BattleGround)_random.Next(Enum.GetValues(typeof(BattleGround)).Length);
                _battleGroundScenarios.TryGetValue(battleGround, out var scenario); //Design pattern: Strategy
                if (scenario == null)
                {
                    throw new Exception("Unsupported battleGround");
                }

                var result = scenario.SimulateWar(warDeclarationRequest.AttackingArmy, warDeclarationRequest.DefendingArmy);
                lock (fightResults) //updated using thread safe mechanisms
                {
                    fightResults.Add(result);
                }
            });

            return _battleAgregatorService.AgregateWarResults(fightResults);
        }
    }
}
