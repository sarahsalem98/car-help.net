using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Areas.Client.Models.ViewModel
{
    public class CarVM
    {
         public string Id { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string ChassisNumber { get; set; }
        [Required]
        public string CarModelId { get; set; }
        [Required]
        public string CarTypeId { get; set; }
    }
}
