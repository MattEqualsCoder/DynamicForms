namespace DynamicForms.Library.Core.Attributes;

public abstract class DynamicFormGroupAttribute(
    DynamicFormLayout layout,
    string name = "",
    string? parentGroup = null,
    int order = 1000,
    DynamicFormPlatform platforms = DynamicFormPlatform.All) : Attribute
{
    public abstract DynamicFormGroupStyle Style { get; }
    public DynamicFormLayout Type => layout;
    public string Name => name;
    public string? ParentGroup => parentGroup;
    public int Order => order;
    public DynamicFormPlatform Platforms => platforms;
}