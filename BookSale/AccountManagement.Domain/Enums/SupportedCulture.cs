using AccountManagement.Domain.Base.Attributes;
using System.Text.Json.Serialization;

namespace AccountManagement.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SupportedCulture
{
    [EnumDescription("en")]
    en = 0,

    [EnumDescription("fa")]
    fa = 1,

    [EnumDescription("ar")]
    ar = 2
}
