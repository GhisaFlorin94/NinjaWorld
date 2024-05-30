namespace NinjaWorld.Domain.Entities
{
    public class Tool
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public Ninja Ninja { get; set; }
        public Guid NinjaId { get; set; }
    }
}