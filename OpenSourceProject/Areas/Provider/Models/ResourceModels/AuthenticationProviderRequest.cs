using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Areas.Provider.Models.ResourceModels
{
    public class AuthenticationProviderRequest
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
