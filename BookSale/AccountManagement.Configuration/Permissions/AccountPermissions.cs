using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Configuration.Permissions
{
    public class AccountPermissions
    {
        public const int ListUsers = 30;
        public const int SearchUser = 31;
        public const int CreateUser = 32;
        public const int EditUser = 33;

        public const int ListRoles = 40;
        public const int SearchRole = 41;
        public const int CreateRole = 42;
        public const int EditRole = 43;
    }
}
