using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WssConsultingTask.Core.Entities
{
    [Serializable, XmlRoot("Departament")]
    public class Departament
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [XmlIgnore]
        public HierarchyId Hierarchy { get; set; }

        public int? ParentId { get; set; }

        [NotMapped]
        public DepartamentLevel Level => (DepartamentLevel)Hierarchy.GetLevel();
    }
}
