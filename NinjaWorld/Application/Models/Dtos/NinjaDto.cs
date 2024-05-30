using NinjaWorld.Domain.Entities;
using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Application.Models.Dtos
{
    public class NinjaDto
    {
        public string Name { get; set; }
        public NinjaRank Rank { get; set; }
        public Village Village { get; set; }
        public int Power { get; set; }
        public ICollection<ToolDto> Tools { get; set; }

        public Ninja ToNinja()
        {
            return new Ninja { Name = Name, Rank = Rank, Village = Village, Power = Power, Tools = new List<Tool>(Tools.Select(t => t.ToTool())) };
        }
    }
}