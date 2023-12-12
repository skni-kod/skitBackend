using skit.Core.Common.DTO;

namespace skit.Core.Common.Extensions;

public static class EnumExtensions
{
    public static List<BaseEnum> GetValues<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Where(x => !x.Equals(default(TEnum)))
            .Select(x => new BaseEnum {Id = Convert.ToInt32(x), Name = x.ToString()})
            .ToList();
    }
    
    public static List<BaseEnum?> GetObjectsFromFlag<TEnum>(this TEnum flagsEnum) where TEnum : Enum
    {
        var result = new List<BaseEnum?>();

        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
        {
            if (value.Equals(Enum.ToObject(typeof(TEnum), 0)))
                continue;
            
            if (flagsEnum.HasFlag(value))
            {
                result.Add(value);
            }
        }
        
        return result;
    }
    
    public static List<TEnum> GetValuesFromFlag<TEnum>(this TEnum flagsEnum) where TEnum : Enum
    {
        var result = new List<TEnum>();

        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
        {
            if (value.Equals(Enum.ToObject(typeof(TEnum), 0)))
                continue;
            
            if (flagsEnum.HasFlag(value))
            {
                result.Add(value);
            }
        }
        
        return result;
    }

    public static TEnum AggregateToFlag<TEnum>(this List<TEnum>? enumValues) where TEnum : Enum
    {
        if (enumValues == null)
            return (TEnum)Enum.ToObject(typeof(TEnum), 0);

        var resultValue = 0;

        foreach (var enumValue in enumValues)
        {
            resultValue |= Convert.ToInt32(enumValue);
        }

        var result = (TEnum)Enum.ToObject(typeof(TEnum), resultValue);
        return result;
    }
}