using System.Reflection;

namespace Playmor_Asp.Helpers;

public class PropertyCopier
{
    public static void CopyProperties<T>(T source, T destination, string[]? excludedProperties = null) where T : class
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .Where(prop => prop.CanRead && prop.CanWrite);

        excludedProperties ??= [];

        foreach (var property in properties)
        {
            if (excludedProperties.Contains(property.Name, StringComparer.CurrentCultureIgnoreCase))
                continue;

            var value = property.GetValue(source);
            property.SetValue(destination, value);
        }
    }
}
