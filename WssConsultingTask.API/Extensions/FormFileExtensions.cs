using System.Text;

namespace WssConsultingTask.API.Extensions
{
    public static class FormFileExtensions
    {
        /// <summary>
        /// Метод расширения для получения xml-разметки из файла.
        /// Чтение файла ведется построчно (на случай, если содержание файла будет слишком большим).
        /// </summary>
        /// <param name="file"> файл </param>
        /// <returns> содержимое файла </returns>
        public async static Task<string> GetContentAsync(this IFormFile file)
        {
            var sb = new StringBuilder();

            using var sr = new StreamReader(file.OpenReadStream());

            while (!sr.EndOfStream) 
            {
                sb.Append(await sr.ReadLineAsync());
            }

            return sb.ToString();
        }
    }
}
