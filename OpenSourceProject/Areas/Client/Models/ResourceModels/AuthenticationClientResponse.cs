
using OpenSourceProject.Models;
using ClientModel = OpenSourceProject.Models.Client;
namespace OpenSourceProject.Areas.Client.Models.ResourceModels
{
    public class AuthenticationClientResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public ClientModel User { get; set; }  
        

    }
}
