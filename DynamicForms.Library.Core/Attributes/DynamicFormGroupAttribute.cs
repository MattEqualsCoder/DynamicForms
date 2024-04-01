namespace DynamicForms.Library.Core.Attributes;

public abstract class DynamicFormGroupAttribute(DynamicFormGroupType type, string name = "", string? parentGroup = null, int order = 1000) : Attribute
{
    public abstract DynamicFormGroupStyle Style { get; }
    public DynamicFormGroupType Type => type;
    public string Name => name;
    public string? ParentGroup => parentGroup;
    public int Order => order;
}