using Microsoft.EntityFrameworkCore;

using WssConsultingTask.Core.Entities;
using WssConsultingTask.Core.Repositories;
using WssConsultingTask.Infrastructure.Data;

namespace WssConsultingTask.Infrastructure.Repositories
{
    public class DepartamentsRepository(ApplicationDbContext dbContext) : IDepartamentsRepository
    {
        public async Task<ICollection<Departament>> GetDepartamentsAsync()
        {
            return await dbContext.Departaments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Departament>> GetAllDescandantDepartamentsAsync(HierarchyId hierarchy)
        {
            return await dbContext.Departaments
                .Where(d => d.Hierarchy.IsDescendantOf(hierarchy))
                .ToListAsync();
        }

        public async Task<Departament?> GetLastDescandantDepartamentAsync(HierarchyId hierarchy)
        {
            return await dbContext.Departaments
                .AsNoTracking()
                .Where(d => d.Hierarchy.GetAncestor(1) == hierarchy)
                .OrderBy(d => d.Hierarchy)
                .LastOrDefaultAsync();
        }

        public async Task<Departament?> GetDepartamentAsync(int departamentId)
        {
            return await dbContext.Departaments.FirstOrDefaultAsync(d => d.Id == departamentId);
        }

        public async Task AddDepartamentsAsync(IEnumerable<Departament> departaments)
        {
            await dbContext.AddRangeAsync(departaments);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDepartamentsAsync(IEnumerable<Departament>? departaments = null)
        {
            dbContext.RemoveRange(departaments ?? dbContext.Departaments);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
