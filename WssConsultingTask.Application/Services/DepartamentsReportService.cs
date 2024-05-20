using WssConsultingTask.Application.Exceptions;
using WssConsultingTask.Application.Interfaces;
using WssConsultingTask.Core.Entities;
using WssConsultingTask.Core.Repositories;

namespace WssConsultingTask.Application.Services
{
    public class DepartamentsReportService(
        IXmlSerializerHelper xmlSerializerHelper,
        IDepartamentsRepository departamentsRepository) : IDepartamentsReportService
    {
        /// <summary>
        /// Метод импорта данных с помощью xml.
        /// </summary>
        /// <param name="xml"> xml-разметка </param>
        /// <returns></returns>
        /// <exception cref="WssApplicationException"></exception>
        public async Task ImportDepartamentsAsync(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new WssApplicationException("Xml is empty.");
            }

            var departaments = xmlSerializerHelper.Deserialize<List<Departament>>(xml);

            if (departaments?.Count > 0)
            {
                //TODO: После десериализации необходимо для каждого элемента в списке departaments заполнить поля: Hierarhy и ParentId.

                await departamentsRepository.DeleteDepartamentsAsync();
                await departamentsRepository.AddDepartamentsAsync(departaments);
            }
        }

        /// <summary>
        /// Метод экспорта данных в xml.
        /// </summary>
        /// <returns> массив байт </returns>
        public async Task<byte[]> ExportDepartamentsAsync()
        {
            var departaments = await departamentsRepository.GetDepartamentsAsync();

            return xmlSerializerHelper.Serialize(departaments);
        }
    }
}
