using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TollFee.Api.Models.ValidationRules
{
    public class ExistingYear : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateTimeRequests = (DateTime[])validationContext.ObjectInstance;
            var _db = (TollDBContext)validationContext.GetService(typeof(TollDBContext));
            var nonExistingYear = false;

            foreach(var dateTimeRequest in dateTimeRequests)
            {
                if (_db.TollFrees.Any(x => x.Year != dateTimeRequest.Year)) 
                { 
                    nonExistingYear = true;
                    break;
                }
            }

            return nonExistingYear 
                ? new ValidationResult("Requested year doesn't exist in the DB")
                : ValidationResult.Success;
        }
    }
}
