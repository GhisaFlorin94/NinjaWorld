using NinjaWorld.Domain.Entities;
using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Application.Models.Dtos
{
    public class ToolDto
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public Tool ToTool()
        {
            return new Tool { Name = Name, Power = Power };
        }
    }
}
