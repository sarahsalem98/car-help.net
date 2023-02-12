using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenSourceProject.Models
{
    public class ProviderBrand
    {

        public string ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        [JsonIgnore]
        public Provider Provider { get; set; }

        public string BrandId { get; set; }
        [ForeignKey("BrandId")]
        public  Brand Brand { get; set; }
    }
}
