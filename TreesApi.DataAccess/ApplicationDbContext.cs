using Microsoft.EntityFrameworkCore;
using TreesApi.DataAccess.Entities;

namespace TreesApi.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TreeNodeEntity> TreeNodes { get; set; }

    public DbSet<JournalEntryEntity> ExceptionJournal { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var assemblyWithConfigurations = GetType().Assembly;
        builder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        base.OnModelCreating(builder);
    }
}
