namespace DynamicForms.Library.Core.Attributes;

public abstract class DynamicFormFieldAttribute(
    string label,
    bool labelIsProperty,
    string? toolTipText = null,
    string? visibleWhenTrue = null,
    string? editableWhenTrue = null,
    string groupName = "",
    int order = int.MaxValue,
    DynamicFormPlatform platforms = DynamicFormPlatform.All)
    : DynamicFormObjectAttribute(groupName, order, platforms)
{
    public abstract DynamicFormFieldType FieldType { get; }
    
    public abstract ICollection<Type>? AllowedTypes { get; }
    
    public string Label { get; } = label;
    public string? ToolTipText { get; } = toolTipText;
    public string? VisibleWhenTrue { get; } = visibleWhenTrue;
    public string? EditableWhenTrue { get; } = editableWhenTrue;

    public bool LabelIsProperty => labelIsProperty;
}