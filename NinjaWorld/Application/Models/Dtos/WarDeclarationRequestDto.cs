using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Application.Models
{
    public class WarDeclarationRequestDto
    {
        public Village AttackingVillage { get; set; }
        public Village DefendingVillage { get; set; }
    }
}
