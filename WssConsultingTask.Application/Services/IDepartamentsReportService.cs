namespace WssConsultingTask.Application.Services
{
    public interface IDepartamentsReportService
    {
        Task ImportDepartamentsAsync(string xml);

        Task<byte[]> ExportDepartamentsAsync();
    }
}
