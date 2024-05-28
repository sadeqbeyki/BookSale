using AccountManagement.Domain.Enums;

namespace AccountManagement.Domain.Const;

public static class AppConstants
{
    public const int DEFAULT_PAGESIZE = 50;
    public const SupportedCulture DEFAULT_CULTURE = SupportedCulture.en;

    public const string JWT_SUB = "sub";

    public const string SQL_AND = " AND ";
    public const string SQL_OR = " OR ";
}
