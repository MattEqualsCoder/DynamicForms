using System.Windows.Input;

namespace DynamicForms.Library.Core.Shared;

public static class TypeExtensions
{
    public static Type GetUnderlyingType(this Type type)
    {
        if (type.IsEnum)
        {
            return typeof(Enum);
        }
        else if (type.GetInterfaces().FirstOrDefault() == typeof(ICommand))
        {
            return typeof(ICommand);
        }
        else if (type.IsGenericType && type.Name.StartsWith("Nullable"))
        {
            return type.GenericTypeArguments[0];
        }
        else
        {
            return type;
        }
    }
}