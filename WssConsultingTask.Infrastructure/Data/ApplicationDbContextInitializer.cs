using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WssConsultingTask.Infrastructure.Data
{
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ApplicationDbContextInitializer> logger;

        public ApplicationDbContextInitializer(
            ApplicationDbContext context,
            ILogger<ApplicationDbContextInitializer> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task MigrateAsync()
        {
            try
            {
                logger.LogInformation("WssConsultingTask DB migration started.");

                await context.Database.MigrateAsync();

                logger.LogInformation("WssConsultingTask DB migration completed.");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migration the WssConsultingTask DB.");
            }
        }
    }
}
