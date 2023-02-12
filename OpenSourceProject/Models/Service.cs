using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenSourceProject.Models
{
    public class Service:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name_AR { get; set; }
        public string Name_EN { get; set; }
        public string ?PhotoUrl { get; set;}
        [JsonIgnore]
        public virtual ICollection<SubService>SubServices { get; set; }
    }
}
