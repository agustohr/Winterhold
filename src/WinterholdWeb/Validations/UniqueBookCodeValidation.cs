using System.ComponentModel.DataAnnotations;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels;

namespace WinterholdWeb.Validations;

public class UniqueBookCodeValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null){
            var _dbContext = (WinterholdContext?)validationContext.GetService(typeof(WinterholdContext))
                ?? throw new NullReferenceException("System Error");

            bool isExist = _dbContext.Books.Any(
                    book=>book.Code == value.ToString()
                );
            if(isExist){
                return new ValidationResult($"Code {value.ToString()} is already exist!");
            }
        }
        return ValidationResult.Success;
    }
}
