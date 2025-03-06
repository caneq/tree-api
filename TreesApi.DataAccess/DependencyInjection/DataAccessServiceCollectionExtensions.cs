using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TreesApi.DataAccess.Entities;
using TreesApi.DataAccess.Repositories;
using TreesApi.DataAccess.Repositories.Generic;
using TreesApi.DataAccess.Repositories.TreeNode;
using TreesApi.DataAccess.UnitsOfWork;

namespace TreesApi.DataAccess.DependencyInjection;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string? connectionString)
    { 
        return services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ITreeNodeRepository, TreeNodeRepository>()
            .AddScoped<IRepository<JournalEntryEntity>, Repository<JournalEntryEntity>>();
    }
}
