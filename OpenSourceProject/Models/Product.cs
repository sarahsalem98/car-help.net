using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{
    public class Product:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        public string Id { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category  { get; set; }

        public string ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        [NotMapped]
        public Provider Provider { get; set; }

        public string Name { get; set; }    
        public string Description { get; set; } 
        
        public string PriceBeforeDiscount { get; set; }
        public string PriceAfterDiscount { get; set;}
        public string Qty { get; set; }
        public  List<string> Image { get; set; }


    }
}
