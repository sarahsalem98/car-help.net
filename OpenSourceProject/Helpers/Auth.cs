using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using OpenSourceProject.Models;
//using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace OpenSourceProject.Helpers
{
    public static class AuthUser
    {
        
        public static string getAuthenticatedUser(this IPrincipal principal)
        {
            var claimIdentity = (ClaimsIdentity)principal.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = claimIdentity.FindFirst(ClaimTypes.Role).Value;
            if(userRole!="Client")
            {
                return null;    
            }
            return userId;

        }
    }


    public  class Auth
    {

     
        public  string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPssword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPssword);
        }

        public  bool ValidatePassword(string hashedPassword, string password)
        {
            if (HashPassword(password) == hashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class ErrorVM
    {
      
       public List< Error> errors { get; set; }
    }

    public class Error
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public ErrorVM signError(IEnumerable<IdentityError> errors)
        {
            List<Error> newError = new List<Error>();
            ErrorVM errorVM=new ErrorVM();
            foreach (var err in errors)
            {
                newError.Add( new Error
                {
                    Message = err.Description,
                    ErrorCode ="400"
                });
            }
            errorVM.errors = newError;
            return errorVM ;
        }
    }
}


//    public class UserValidator : IIdentityValidator <ApplicationUser>
//    {
        
//        public async Task<IdentityResult> ValidateAsync(ApplicationUser item)
//        {
//            if (item.UserName.ToString().Length <7)
//            {
//                var error = new List<Error>();
//                error.Add(new Error
//                {
//                    Message="fdds",
//                    Status="300"
//                });
//                List<string> stringlist = error.Select(x => x.ToString())
//                                    .ToList();

//                return IdentityResult.Failed(stringlist.ToArray());
//            }

//            return IdentityResult.Success;
//        }
//    }
//}
