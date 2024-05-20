using System.ComponentModel;

namespace WssConsultingTask.Core.Entities
{
    public enum DepartamentLevel : byte
    {
        [Description("Компания")]
        Company = 1,

        [Description("Департамент")]
        Departament,

        [Description("Отдел")]
        Division,
    }
}
