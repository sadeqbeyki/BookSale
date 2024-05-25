using AppFramework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class EditRole : CreateRole
    {
        public int Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }

        public EditRole()
        {
            Permissions = new List<int>();
        }
    }
}
