namespace DynamicForms.Library.Core.Attributes;

public abstract class DynamicFormFieldAttribute(
    string labelText,
    string? toolTipText = null,
    string? visibleWhenTrue = null,
    string? editableWhenTrue = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormObjectAttribute(groupName, order)
{
    public abstract DynamicFormFieldType FieldType { get; }
    
    public abstract ICollection<Type>? AllowedTypes { get; }
    
    public string LabelText { get; } = labelText;
    public string? ToolTipText { get; } = toolTipText;
    public string? VisibleWhenTrue { get; } = visibleWhenTrue;
    public string? EditableWhenTrue { get; } = editableWhenTrue;
}