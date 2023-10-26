using System.ComponentModel;

namespace skit.Core.Companies.Enums;

public enum CompanySize
{
    NoData = 0,
    [Description("1-50")]
    Small = 1,
    [Description("50-300")]
    Medium = 2,
    [Description("300+")]
    Large = 3
}