namespace WarResolverClient.Models
{
    public class WarResult
    {
        public List<Ninja> DefendingArmyCasualties { get; set; } = [];
        public List<Ninja> AttackingArmyCasualties { get; set; } = [];
        public Village Winner { get; set; }
        public Village Loser { get; set; }
    }
}