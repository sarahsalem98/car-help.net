using providerModel = OpenSourceProject.Models.Provider;
namespace OpenSourceProject.Areas.Provider.Models.ResourceModels
{
    public class AuthenticationProviderResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public providerModel User { get; set; }
    }
}
