namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage((AttributeTargets.Event))]
public class DynamicFormFieldButtonAttribute(
    string buttonText,
    string labelText = "",
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.Button;

    public string ButtonText = buttonText;
}