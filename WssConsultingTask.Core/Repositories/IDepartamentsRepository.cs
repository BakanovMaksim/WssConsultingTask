using Microsoft.EntityFrameworkCore;

using WssConsultingTask.Core.Entities;

namespace WssConsultingTask.Core.Repositories
{
    public interface IDepartamentsRepository
    {
        Task<ICollection<Departament>> GetDepartamentsAsync();

        Task<ICollection<Departament>> GetAllDescandantDepartamentsAsync(HierarchyId hierarchy);

        Task<Departament?> GetLastDescandantDepartamentAsync(HierarchyId hierarchy);

        Task<Departament?> GetDepartamentAsync(int departamentId);

        Task AddDepartamentsAsync(IEnumerable<Departament> departaments);

        Task DeleteDepartamentsAsync(IEnumerable<Departament>? departaments = null);

        Task SaveChangesAsync();
    }
}
