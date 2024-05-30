using Microsoft.EntityFrameworkCore;
using NinjaWorld.Application.Interfaces;
using NinjaWorld.Domain.Entities;

namespace NinjaWorld.Infrastructure.Data
{
    public class NinjaDbContext : DbContext, INinjaDbContext
    {
        public NinjaDbContext(DbContextOptions<NinjaDbContext> options) : base(options)
        {
        }

        public DbSet<Ninja> Ninja { get; set; }
        public DbSet<Tool> Tool { get; set; }
    }
}