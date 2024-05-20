namespace WssConsultingTask.Application.Models.Responses
{
    public class DepartamentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<DepartamentResponse> Departaments { get; set; } = Array.Empty<DepartamentResponse>();
    }
}
