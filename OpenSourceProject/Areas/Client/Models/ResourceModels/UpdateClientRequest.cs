using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Areas.Client.Models.ResourceModels
{
    public class UpdateClientRequest
    {
        [Required]
        public string  Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int ?CityId { get; set; } 
    }
}
