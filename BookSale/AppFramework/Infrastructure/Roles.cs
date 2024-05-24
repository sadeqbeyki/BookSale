namespace AppFramework.Infrastructure;

public static class Roles
{
    public const string Administrator = "1";
    public const string SystemUser = "2";
    public const string ContentUploader = "3";
    public const string ColleagueUser = "4";

    public static string GetRoleBy(long id)
    {
        return id switch
        {
            1 => "Administrator",
            2 => "Registred User",
            3 => "Content Manager",
            4 => "Colleague User",
            _ => "",
        };
    }
}
