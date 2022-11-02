using AccountManagement.Domain.RoleAgg;
using AppFramework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    //public partial class Account : EntityBase
    public class Account : EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; set; }
        public string ProfilePhoto { get; private set; }

        public Account(string fullName, string userName, string password, string mobile, long roleId, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;

            //کاربر پیشفرض کاربر سیستم می باشد
            if (roleId == 0)
                RoleId = 2;

            ProfilePhoto = profilePhoto;
        }
        public void Edit(string fullName, string userName, string mobile, long roleId, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Mobile = mobile;
            RoleId = roleId;
            if(!string.IsNullOrEmpty(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
