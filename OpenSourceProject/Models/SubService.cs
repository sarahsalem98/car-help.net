using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{
    public class SubService:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        public string ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }  

        public string Name_AR { get; set; }
        public string Name_EN { get; set; }
        public string ?PhotoUrl { get; set; }

        public IList<ProviderService> ProviderServices { get; set; }

    }
}
