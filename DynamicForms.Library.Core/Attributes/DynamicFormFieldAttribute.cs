namespace DynamicForms.Library.Core.Attributes;

public abstract class DynamicFormFieldAttribute(
    string labelText,
    string? toolTipText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormObjectAttribute(groupName, order)
{
    public abstract DynamicFormFieldType FieldType { get; }
    
    public string LabelText { get; } = labelText;
    public string? ToolTipText { get; } = toolTipText;
    public string? VisibleWhenProperty { get; } = visibleWhenProperty;
    public string? EditableWhenProperty { get; } = editableWhenProperty;
}