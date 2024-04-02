namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a control for a argb byte array from a hexadecimal value entered in a text box
/// </summary>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenTrue">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenTrue">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldColorPickerAttribute(
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenTrue = null,
    string? editableWhenTrue = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenTrue, editableWhenTrue, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.ColorPicker;
    public override ICollection<Type>? AllowedTypes => [typeof(byte[])];
}