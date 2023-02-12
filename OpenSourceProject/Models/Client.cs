using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{
    public class Client:ApplicationUser
    {
       
        public string? ProfilePhotoUrl { get; set; }
        public  int CityId { get; set; }  
        [ForeignKey("CityId")]
        public  City City { get; set; }
    }
}
