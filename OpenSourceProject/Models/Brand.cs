using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenSourceProject.Models
{
    public class Brand : TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        public string Id { get; set; }
        public string Name_AR { get; set; }
        public string Name_EN { get; set; }
        [JsonIgnore]
        public IList<ProviderBrand> ProviderBrands {get;set;}
    }
}
