using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppFramework.Application
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        //private readonly string[] _extensions;
        //public AllowedExtensionsAttribute(string[] extensions)
        //{
        //    _extensions = extensions;
        //}

        //protected override ValidationResult IsValid(
        //object value, ValidationContext validationContext)
        //{
        //    var file = value as IFormFile;
        //    var extension = Path.GetExtension(file.FileName);
        //    if (file != null)
        //    {
        //        if (!_extensions.Contains(extension.ToLower()))
        //        {
        //            return new ValidationResult(GetErrorMessage(file.FileName));
        //        }
        //    }

        //    return ValidationResult.Success;
        //}

        //public string GetErrorMessage(string name)
        //{
        //    return $"{name} extension is not allowed!";
        //}


    }
}
