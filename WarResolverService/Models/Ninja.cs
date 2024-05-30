namespace WarResolverClient.Models
{
    public class Ninja
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public NinjaRank Rank { get; set; }
        public Village Village { get; set; }
        public int Power { get; set; }
        public ICollection<Tool> Tools { get; set; } = [];
    }
}