using Microsoft.AspNetCore.Identity;

namespace OpenSourceProject.Helpers
{
    public class Roles
    {
        public  IdentityRole Client = new IdentityRole("Client");
        public IdentityRole Provider = new IdentityRole("Provider");
        public IdentityRole Admin =new IdentityRole("Admin");   


    }
}
