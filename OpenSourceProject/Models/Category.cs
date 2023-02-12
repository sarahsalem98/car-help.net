using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Models
{
    public class Category:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        public string Id { get; set; }
        public string Name_AR { get; set; }
        public string Name_EN { get; set; }

       // public IList<Product> Products { get; set; }    
    }
}
