namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a basic text block to display static text
/// </summary>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenProperty">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenProperty">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldTextAttribute(
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.Text;
    public override ICollection<Type>? AllowedTypes => null;
}