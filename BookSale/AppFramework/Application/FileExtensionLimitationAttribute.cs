using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppFramework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;

        public FileExtensionLimitationAttribute(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            var fileExtension = Path.GetExtension(file.FileName);
            if (file == null) return true;

            return _validExtensions.Contains(fileExtension);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage);
        }
    }
}
