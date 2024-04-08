namespace DynamicForms.Library.Core.Attributes;

public class DynamicFormObjectAttribute (string groupName = "",
    int order = int.MaxValue, DynamicFormPlatform platforms = DynamicFormPlatform.All) : Attribute
{
    public string GroupName => groupName;
    public int Order => order;
    public DynamicFormPlatform Platforms => platforms;
}