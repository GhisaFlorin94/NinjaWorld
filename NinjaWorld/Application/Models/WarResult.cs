using NinjaWorld.Domain.Entities;
using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Application.Models
{
    public class WarResult
    {
        public List<Ninja> AttackingArmyCasualties { get; set; } = [];
        public List<Ninja> DefendingArmyCasualties { get; set; } = [];
        public Village Winner { get; set; }
        public Village Loser { get; set; }
    }
}