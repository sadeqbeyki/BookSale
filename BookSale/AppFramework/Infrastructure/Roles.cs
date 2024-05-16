namespace AppFramework.Infrastructure;

public static class Roles
{
    public const string Administrator = "10002";
    public const string SystemUser = "10003";
    public const string ContentUploader = "10004";
    public const string ColleagueUser = "10005";

    public static string GetRoleBy(long id)
    {
        return id switch
        {
            10002 => "Administrator",
            10003 => "Registred User",
            10004 => "Content Manager",
            10005 => "Colleague User",
            _ => "",
        };
    }
}
