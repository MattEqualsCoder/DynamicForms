namespace DynamicForms.Library.Core.Shared;

public static class TypeExtensions
{
    public static Type GetUnderlyingType(this Type type)
    {
        if (type.IsEnum)
        {
            return typeof(Enum);
        }
        else if (!type.IsGenericType)
        {
            return type;
        }
        else
        {
            return type.GenericTypeArguments[0];
        }
    }
}