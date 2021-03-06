using AccountManagement.Application.Contracts.Role;
using AppFramework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FullName { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long RoleId { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
