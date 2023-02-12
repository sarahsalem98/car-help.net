using System.ComponentModel.DataAnnotations;

namespace OpenSourceProject.Areas.Provider.Models.ResourceModels
{
    public class RegisterProviderWorkHoursRequest: IValidatableObject
    {

        public  string Day_EN { get; set; }
        public string Day_AR { get; set;}
        public TimeSpan From { get; set;}
        public TimeSpan To { get; set;} 
        public bool IsClosed { get; set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if(From!=null && To!=null && From>=To)
                yield return new ValidationResult("EndDate is not greater than StartDate.");
        }
    }
}
