using AccountManagement.Domain.AccountAgg;
using AppFramework.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public Permission(int code)
        {
            Code = code;
        }
    }
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Permission> Permissions { get; private set; }
        public List<Account> Accounts { get; private set; }

        protected Role()
        {
        }
        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            Accounts = new List<Account>();
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
