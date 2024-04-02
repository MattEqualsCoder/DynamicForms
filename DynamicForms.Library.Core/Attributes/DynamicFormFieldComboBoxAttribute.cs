namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a combo box for either a string or enum value
/// </summary>
/// <param name="comboBoxOptionsProperty">Property to reference for a list of options for a string combo box</param>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenTrue">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenTrue">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldComboBoxAttribute(
    string? comboBoxOptionsProperty = null,
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenTrue = null,
    string? editableWhenTrue = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenTrue, editableWhenTrue, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.ComboBox;
    public override ICollection<Type>? AllowedTypes => [typeof(string), typeof(Enum)];

    public string? ComboBoxOptionsProperty { get; } = comboBoxOptionsProperty;
}