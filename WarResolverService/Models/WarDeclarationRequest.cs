namespace WarResolverClient.Models
{
    internal class WarDeclarationRequest
    {
        public List<Ninja> AttackingArmy { get; set; } = [];
        public List<Ninja> DefendingArmy { get; set; } = [];
        public Village AttackingVillage { get; set; }
        public Village DefendingVillage { get; set; }

    }
}
