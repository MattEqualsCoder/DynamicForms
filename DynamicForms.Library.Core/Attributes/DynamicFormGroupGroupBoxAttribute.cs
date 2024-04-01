namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DynamicFormGroupGroupBoxAttribute(DynamicFormGroupType type, string name = "", string? parentGroup = null, int order = int.MaxValue) : DynamicFormGroupAttribute(type, name, parentGroup, order)
{
    public override DynamicFormGroupStyle Style => DynamicFormGroupStyle.GroupBox;
}