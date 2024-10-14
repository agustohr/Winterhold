using System.ComponentModel.DataAnnotations;
using WinterholdDataAccess.Models;

namespace WinterholdWeb.Validations;

public class UniqueMembershipNumberValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null){
            var _dbContext = (WinterholdContext?)validationContext.GetService(typeof(WinterholdContext))
                ?? throw new NullReferenceException("System Error");

            bool isExist = _dbContext.Customers.Any(
                    book=>book.MembershipNumber == value.ToString()
                );
            if(isExist){
                return new ValidationResult($"Membership Number {value.ToString()} is already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
