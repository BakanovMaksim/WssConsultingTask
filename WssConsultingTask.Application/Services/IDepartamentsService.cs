using WssConsultingTask.Application.Models.Responses;

namespace WssConsultingTask.Application.Services
{
    public interface IDepartamentsService
    {
        Task<ICollection<DepartamentResponse>> GetDepartamentsAsync();

        Task AddDepartamentAsync(int departamentParentId);

        Task MoveDepartamentAsync(int movingDepartamentId, int newDepartamentParentId);

        Task DeleteDepartamentAsync(int departamentId);
    }
}
