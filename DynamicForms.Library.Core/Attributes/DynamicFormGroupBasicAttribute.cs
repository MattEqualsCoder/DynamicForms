namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DynamicFormGroupBasicAttribute(DynamicFormGroupType type, string name = "", string? parentGroup = null, int order = 1000) : DynamicFormGroupAttribute(type, name, parentGroup, order)
{
    public override DynamicFormGroupStyle Style => DynamicFormGroupStyle.Basic;
}