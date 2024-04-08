namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a checkbox control for a boolean variable
/// </summary>
/// <param name="checkBoxText">The text to display for the check box</param>
/// <param name="alignment">The alignment of the button</param>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenTrue">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenTrue">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
/// <param name="platforms">The platform(s) the object should be displayed on</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldCheckBoxAttribute(
    string checkBoxText,
    DynamicFormAlignment alignment = DynamicFormAlignment.Default,
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenTrue = null,
    string? editableWhenTrue = null,
    string groupName = "",
    int order = int.MaxValue,
    DynamicFormPlatform platforms = DynamicFormPlatform.All)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenTrue, editableWhenTrue, groupName, order, platforms)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.CheckBox;
    public override ICollection<Type> AllowedTypes => [typeof(bool)];

    public string CheckBoxText = checkBoxText;

    public DynamicFormAlignment Alignment => alignment;
}