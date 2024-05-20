using Microsoft.AspNetCore.Mvc;

using WssConsultingTask.API.Extensions;
using WssConsultingTask.Application.Services;

namespace WssConsultingTask.API.Controllers
{
    [Route("departaments/report")]
    public class DepartamentsReportController(IDepartamentsReportService departamentsReportService) : Controller
    {
        [HttpPost("import")]
        public async Task ImportXml(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                throw new ArgumentException("Incorrect file", nameof(file));
            }

            var xml = await file.GetXmlAsync();

            await departamentsReportService.ImportDepartamentsAsync(xml);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportXml()
        {
            var fileBytes = await departamentsReportService.ExportDepartamentsAsync();

            return File(
                fileContents: fileBytes,
                contentType: "application/xml",
                fileDownloadName: $"departaments_{DateTime.UtcNow:f}");
        }
    }
}
