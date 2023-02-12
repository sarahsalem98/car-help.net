using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Areas.Client.Models.ResourceModels
{
    public class AuthenticationClientRequest
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
