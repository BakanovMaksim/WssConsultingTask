using WssConsultingTask.Application.Exceptions;
using WssConsultingTask.Application.Extensions;
using WssConsultingTask.Application.Models.Responses;
using WssConsultingTask.Core.Entities;
using WssConsultingTask.Core.Repositories;

namespace WssConsultingTask.Application.Services
{
    public class DepartamentsService(IDepartamentsRepository departamentsRepository) : IDepartamentsService
    {
        /// <summary>
        /// Метод получения иерархического справочника департаментов (рекурсивно).
        /// </summary>
        /// <returns> Список департаментов </returns>
        public async Task<ICollection<DepartamentResponse>> GetDepartamentsAsync()
        {
            //TODO: необходима пагинация или скролл для получения списка департаментов (чтобы не нагружать бд тяжелыми запросами, если список очень большой).

            //TODO: возможно, стоит подумать в сторону кэширования (соответственно, еще понадобится инвалидация кэша), если список департаментов редактируют очень редко.

            var allDepartaments = await departamentsRepository.GetDepartamentsAsync();
            var departamentRootId = allDepartaments.FirstOrDefault(d => !d.ParentId.HasValue)?.Id ?? 0;
            var departamentsByParentIds = allDepartaments.ToLookup(d => d.ParentId);

            return GetDescendtantDepartaments(departamentRootId);

            ICollection<DepartamentResponse> GetDescendtantDepartaments(int departamentId)
            {
                var descendtantDepartaments = departamentsByParentIds[departamentId].ToList();
                var departamentName = descendtantDepartaments.FirstOrDefault()?.Level.ToDescription();

                return descendtantDepartaments.Count > 0
                    ? descendtantDepartaments
                        .OrderBy(d => d.Hierarchy)
                        .Select((d, i) => new DepartamentResponse
                        {
                            Id = d.Id,
                            Name = $"{departamentName} {i + 1}",
                            Departaments = GetDescendtantDepartaments(d.Id),
                        })
                        .ToList()
                    : [];
            }
        }

        /// <summary>
        /// Метод добавления департамента.
        /// </summary>
        /// <param name="departamentParentId"> идентификатор родительского департамента </param>
        /// <returns></returns>
        public async Task AddDepartamentAsync(int departamentParentId)
        {
            //TODO: не хватает валидации идентификатора departamentParentId на уровень иерархии.
            //(т.е. кейс, когда родительский элемент является самым нижним уровнем и не может иметь потомков).

            var departamentParent = await GetAndValidateDepartamentAsync(departamentParentId);
            var lastDescendantDepartament = await departamentsRepository.GetLastDescandantDepartamentAsync(departamentParent.Hierarchy);

            var departamentToAdd = new Departament
            {
                ParentId = departamentParentId,
                Hierarchy = departamentParent.Hierarchy.GetDescendant(lastDescendantDepartament?.Hierarchy),
            };

            await departamentsRepository.AddDepartamentsAsync([departamentToAdd]);
        }

        /// <summary>
        /// Метод перемещения департамента от одного родительского элемента к другому.
        /// Из бд достаются перемещаемый и новый родительский элементы.
        /// Из бд достаются потомки всех уровней для перемещаемого элемента.
        /// В цикле для перемещаемого элемента и всех его потомков перезаписывается поле Hierarchy.
        /// После цикла для перемещаемого элемента перезаписывается поле parentId на идентификатор нового родительского элемента.
        /// </summary>
        /// <param name="movingDepartamentId"> идентификатор перемещаемого элемента </param>
        /// <param name="newDepartamentParentId"> идентификатор нового родительского элемента </param>
        /// <returns></returns>
        public async Task MoveDepartamentAsync(int movingDepartamentId, int newDepartamentParentId)
        {
            //TODO: не хватает валидации идентификатора departamentParentId на уровень иерархии.
            //(т.е. кейс, когда родительский элемент является самым нижним уровнем и не может иметь потомков).

            var movingDepartament = await GetAndValidateDepartamentAsync(movingDepartamentId);
            var newDepartamentParent = await GetAndValidateDepartamentAsync(newDepartamentParentId);
            var descendantsToMove = await departamentsRepository.GetAllDescandantDepartamentsAsync(movingDepartament.Hierarchy);

            var movingDepartamentHierarhy = movingDepartament.Hierarchy.GetAncestor(1);

            foreach (var descendant in descendantsToMove)
            {
                descendant.Hierarchy = descendant.Hierarchy
                    .GetReparentedValue(
                        movingDepartamentHierarhy,
                        newDepartamentParent.Hierarchy) ?? movingDepartament.Hierarchy;
            }

            movingDepartament.ParentId = newDepartamentParent.Id;

            await departamentsRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Метод удаления департамента.
        /// </summary>
        /// <param name="departamentId"> идентификатор департамента </param>
        /// <returns></returns>
        public async Task DeleteDepartamentAsync(int departamentId)
        {
            var departamentToDelete = await GetAndValidateDepartamentAsync(departamentId);
            var departamentsToDelete = await departamentsRepository.GetAllDescandantDepartamentsAsync(departamentToDelete.Hierarchy);

            await departamentsRepository.DeleteDepartamentsAsync(departamentsToDelete);
        }

        private async Task<Departament> GetAndValidateDepartamentAsync(int departamentId) =>
            await departamentsRepository.GetDepartamentAsync(departamentId)
                ?? throw new WssApplicationException(ApplicationErrorCode.EntityNotFound, $"Departament with id={departamentId} not found.");
    }
}
