using System.ComponentModel;

namespace RentManager.Common.Helpers;

public static class EnumHelper
{
    // Get the display name of the enum value
    public static string DisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DisplayNameAttribute)field.GetCustomAttributes(typeof(DisplayNameAttribute), false)[0];
        return attribute.DisplayName;
    }

    // Get the description of the enum value
    public static string Description(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
        return attribute.Description;
    }

    // Check if an enum value has a flag set
    public static bool HasFlag(this Enum value, Enum flag)
    {
        if (value.GetType() != flag.GetType())
        {
            throw new ArgumentException("Argument type mismatch.");
        }
        return (Convert.ToInt64(value) & Convert.ToInt64(flag)) != 0;
    }

    // Get the values of the enum as an array
    public static T[] GetValuesArray<T>() where T : Enum
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    // Get the values of the enum as an IEnumerable

    public static IEnumerable<T> GetValuesList<T>() where T : Enum
    {
        return ((T[])Enum.GetValues(typeof(T))).ToList();
    }

    public static IEnumerable<string> GetValuesStringList<T>() where T : Enum
    {
        List<string> strings = new List<string>();
        var list = GetValuesList<T>();
        foreach (var value in list)
        {
            strings.Add(value.ToString());
        }
        return strings;
    }
}