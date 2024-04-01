using System.Reflection;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Library.Core;

public class DynamicFormField(object parent, object? value, PropertyInfo? property, DynamicFormFieldAttribute attribute, string groupName) : DynamicFormObject
{
    public override bool IsGroup => false;

    public object ParentObject => parent;
    
    public object? Value { get; set; } = value;
    public PropertyInfo? Property => property;
    public DynamicFormFieldAttribute Attributes => attribute;
    public override string ParentGroupName => groupName;
    public DynamicFormFieldType Type => attribute.FieldType;

    public string PropertyName => property?.Name ?? "";

    public void SetValue(object? parentObject, object? value)
    {
        property?.SetValue(parentObject, value);
    }

    public object? GetValue(object? parentObject)
    {
        return property?.GetValue(parentObject);
    }
}