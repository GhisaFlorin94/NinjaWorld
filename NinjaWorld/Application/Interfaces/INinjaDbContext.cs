using Microsoft.EntityFrameworkCore;
using NinjaWorld.Domain.Entities;

namespace NinjaWorld.Application.Interfaces;

public interface INinjaDbContext
{
    DbSet<Ninja> Ninja { get; set; }
    DbSet<Tool> Tool { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public int SaveChanges();
}