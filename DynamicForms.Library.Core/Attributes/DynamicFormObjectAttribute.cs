namespace DynamicForms.Library.Core.Attributes;

public class DynamicFormObjectAttribute (string groupName = "",
    int order = int.MaxValue) : Attribute
{
    public string GroupName => groupName;
    public int Order => order;
}