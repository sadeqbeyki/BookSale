using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.AccountAgg
{
    //public partial class Account : EntityBase
    public class Account : IdentityUser<long>
    {
        public string FullName { get; private set; }
        public string Password { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; set; }
        public string ProfilePhoto { get; private set; }
        public DateTime CreationDate { get; set; }

        public Account(string fullName, string userName, string password, string mobile, long roleId, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Password = password;
            PhoneNumber = mobile;
            RoleId = roleId;

            //کاربر پیشفرض کاربر سیستم می باشد
            if (roleId == 0)
                RoleId = 2;

            ProfilePhoto = profilePhoto;
            CreationDate = DateTime.Now;
        }
        public void Edit(string fullName, string userName, string mobile, long roleId, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            PhoneNumber = mobile;
            RoleId = roleId;
            if (!string.IsNullOrEmpty(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
