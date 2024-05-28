using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.Entities.RoleAgg;

public class ApplicationRole : IdentityRole<int>
{
    public DateTime CreationDate { get; private set; }
    public List<Permission> Permissions { get; private set; }

    protected ApplicationRole()
    {
    }
    public ApplicationRole(string name, List<Permission> permissions)
    {
        Name = name;
        Permissions = permissions;
        CreationDate = DateTime.Now;
    }

    public void Edit(string name, List<Permission> permissions)
    {
        Name = name;
        Permissions = permissions;
    }
}
