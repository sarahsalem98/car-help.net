namespace OpenSourceProject.Areas.Client.Models.ResourceModels
{
    public class RegisterClientRequest
    {
        public string Name { get; set; }    
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; } 
        
    }
}
