using WssConsultingTask.Infrastructure.Data;

namespace WssConsultingTask.API.Extensions
{
    public static class DatabaseInitializerExtensions
    {
        public static IApplicationBuilder UseMigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

            initializer.MigrateAsync().GetAwaiter().GetResult();

            return app;
        }
    }
}
