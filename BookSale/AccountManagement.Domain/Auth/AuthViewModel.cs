namespace AccountManagement.Domain.Auth;

public class AuthViewModel
{
    public long Id { get; set; }
    public List<int> Roles { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Mobile { get; set; }
    public List<int> Permissions { get; set; }

    public AuthViewModel() { }

    public AuthViewModel(long id, string fullName, string userName, string mobile, List<int> permissions)
    {
        Id = id;

        FullName = fullName;
        UserName = userName;
        Mobile = mobile;
        Permissions = permissions;
    }
}