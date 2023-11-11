namespace skit.Core.Common.DTO;

public sealed class BaseEnum
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public static implicit operator BaseEnum(Enum enumObject)
    {
        return new BaseEnum
        {
            Id = Convert.ToInt32(enumObject),
            Name = enumObject.ToString()
        };
    }
}