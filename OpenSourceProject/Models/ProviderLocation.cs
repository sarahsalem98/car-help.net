using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Models
{
    public class ProviderLocation:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        public string Id { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public string ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }  

        public string Lat { get; set; }
        public string Long { get; set; }    

    }
}
