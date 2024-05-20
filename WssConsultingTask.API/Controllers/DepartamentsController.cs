using Microsoft.AspNetCore.Mvc;

using WssConsultingTask.Application.Services;

namespace WssConsultingTask.API.Controllers
{
    public class DepartamentsController(IDepartamentsService departamentsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var departaments = await departamentsService.GetDepartamentsAsync();

            return View(departaments);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartament(int departamentParentId)
        {
            await departamentsService.AddDepartamentAsync(departamentParentId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPut("move")]
        public async Task<ActionResult> MoveDepartament(int movingDepartamentId, int newDepartamentParentId)
        {
            await departamentsService.MoveDepartamentAsync(movingDepartamentId, newDepartamentParentId);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartament(int departamentId)
        {
            await departamentsService.DeleteDepartamentAsync(departamentId);

            return RedirectToAction(nameof(Index));
        }
    }
}