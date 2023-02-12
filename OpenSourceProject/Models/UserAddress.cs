using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{
    public class UserAddress :TimeStamp
    {
        public int id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public string Note { get; set; }    
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Lat { get; set; }
        public string Long { get; set; }    
        public string Address { get; set; }

        
    }
}
