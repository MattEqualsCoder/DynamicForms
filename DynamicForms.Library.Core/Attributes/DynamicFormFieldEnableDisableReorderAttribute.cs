namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldEnableDisableReorderAttribute(
    string optionsProperty,
    string labelText = "",
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.EnableDisableReorderList;

    public string OptionsProperty => optionsProperty;
}