namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldTextBoxAttribute(
    string labelText = "",
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.TextBox;
}