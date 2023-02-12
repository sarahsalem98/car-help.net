using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{
    public class Car:TimeStamp
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string ?ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public string Name { get; set; }   
        public string ChassisNumber { get; set; }

        public string CarModelId { get; set; }
        [ForeignKey("CarModelId")]
       
        public  CarModel CarModel { get; set; }

        public string CarTypeId { get; set; }
        [ForeignKey("CarTypeId")]
        public  CarType CarType { get; set; }
    }
}
