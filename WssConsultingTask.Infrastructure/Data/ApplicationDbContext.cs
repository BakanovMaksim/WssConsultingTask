using Microsoft.EntityFrameworkCore;

using WssConsultingTask.Core.Entities;

namespace WssConsultingTask.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Departament> Departaments => Set<Departament>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
