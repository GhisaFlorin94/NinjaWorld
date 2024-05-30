using NinjaWorld.Application.Models.Orders;
using NinjaWorld.Domain.Enums;

namespace NinjaWorld.Presentation.Requests
{
    public class SearchNinjaQuery
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public NinjaRank? Rank { get; set; }
        public Village? Village { get; set; }
        public int? Power { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
