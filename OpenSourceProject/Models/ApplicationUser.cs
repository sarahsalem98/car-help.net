using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace OpenSourceProject.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class ApplicationUser:IdentityUser
    {
       
       public string? ApiToken { get; set; }
       public string? DeviceToken { get; set; } 
       public string ? AuthStatus { get; set; }
       public  string ? WhatsAppNumber { get; set; }
       public string? Name { get; set; }

        [DefaultValue(true)]
        public bool IsSuspended { get; set; }

        public  UserAddress providerAddress { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }




    }
}
