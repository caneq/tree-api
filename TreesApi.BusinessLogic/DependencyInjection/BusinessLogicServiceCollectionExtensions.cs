using Microsoft.Extensions.DependencyInjection;
using TreesApi.BusinessLogic.Factories;
using TreesApi.BusinessLogic.MappingProfiles;
using TreesApi.BusinessLogic.Providers;
using TreesApi.BusinessLogic.Services.Journal;
using TreesApi.BusinessLogic.Services.Tree;
using TreesApi.BusinessLogic.Validators;
using TreesApi.DataAccess.DependencyInjection;

namespace TreesApi.BusinessLogic.DependencyInjection;

public static class BusinessLogicServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, string? connectionString)
    {
        return services.AddDataAccess(connectionString)
            .AddAutoMapper(typeof(BusinessLogicMappingProfile))
            .AddScoped<IEventIdProvider, GuidEventIdProvider>()
            .AddScoped<ITreeService, TreeService>()
            .AddScoped<IJournalService, JournalService>()
            .AddTransient<IErrorDetailsFactory, ErrorDetailsFactory>()
            .AddTransient<ITreeUpdateValidator, TreeUpdateValidator>();
    }
}
