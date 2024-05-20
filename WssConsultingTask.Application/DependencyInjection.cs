using Microsoft.Extensions.DependencyInjection;

using WssConsultingTask.Application.Services;

namespace WssConsultingTask.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartamentsService, DepartamentsService>();
            services.AddScoped<IDepartamentsReportService, DepartamentsReportService>();

            return services;
        }
    }
}
