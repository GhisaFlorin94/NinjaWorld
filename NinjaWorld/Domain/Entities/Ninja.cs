using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Domain.Entities
{
    public class Ninja
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public NinjaRank Rank { get; set; }
        public Village Village { get; set; }
        public int Power { get; set; }
        public ICollection<Tool> Tools { get; set; }
    }
}