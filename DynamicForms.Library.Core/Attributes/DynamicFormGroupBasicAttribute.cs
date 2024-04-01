namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for a group without any styling
/// </summary>
/// <param name="layout">The layout of the controls within the group</param>
/// <param name="name">The name of the group</param>
/// <param name="parentGroup">The parent group for this group to be under, if any</param>
/// <param name="order">The order in which to display the group</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DynamicFormGroupBasicAttribute(DynamicFormLayout layout, string name = "", string? parentGroup = null, int order = 1000) : DynamicFormGroupAttribute(layout, name, parentGroup, order)
{
    public override DynamicFormGroupStyle Style => DynamicFormGroupStyle.Basic;
}