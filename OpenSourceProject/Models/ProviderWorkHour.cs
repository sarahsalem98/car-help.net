using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OpenSourceProject.Models
{
    public class ProviderWorkHour:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        public string Id { get; set; }

        public string ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }

        public string Day_EN { get; set; }  
        public string Day_AR { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm:ss}")]
        [DataType(DataType.Time)]
        public TimeSpan From { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public TimeSpan To { get; set; }

        [DefaultValue(true)]
        public bool IsClosed { get; set; }


    }
}
