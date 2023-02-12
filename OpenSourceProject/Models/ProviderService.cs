using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenSourceProject.Models
{
    public class ProviderService:TimeStamp
    {
    

        public string ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        [JsonIgnore]
        public virtual Provider Provider { get; set; }

        public string SubServiceId { get; set; }
        [ForeignKey("SubServiceId")]
        public virtual  SubService SubService { get; set; }

    }
}
