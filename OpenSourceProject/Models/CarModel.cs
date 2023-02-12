using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{
    public class CarModel:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        public string Id { get; set; }

        public string Name_AR { get; set; }
        public string Name_EN { get; set; }
    }
}
