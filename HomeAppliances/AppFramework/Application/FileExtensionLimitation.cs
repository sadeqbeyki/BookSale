using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppFramework.Application
{
    public class FileExtensionLimitation : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;

        public FileExtensionLimitation(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }
        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            var supportedTypes = new[] { ".jpg", ".png", "jpeg" };
            var fileExtension = Path.GetExtension(file.FileName);
            if (file == null && supportedTypes.Contains(fileExtension)) return true;


            return _validExtensions.Contains(fileExtension);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage);
        }




    }
}
