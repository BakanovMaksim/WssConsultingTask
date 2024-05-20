using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WssConsultingTask.Application.Interfaces;
using WssConsultingTask.Core.Repositories;
using WssConsultingTask.Infrastructure.Data;
using WssConsultingTask.Infrastructure.Helpers;
using WssConsultingTask.Infrastructure.Repositories;

namespace WssConsultingTask.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = config.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString, x => x.UseHierarchyId());
            });
            services.AddTransient<IXmlSerializerHelper, XmlSerializerHelper>();
            services.AddScoped<ApplicationDbContextInitializer>();
            services.AddScoped<IDepartamentsRepository, DepartamentsRepository>();

            return services;
        }
    }
}
