namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for a group that can be hidden/shown
/// </summary>
/// <param name="layout">The layout of the controls within the group</param>
/// <param name="name">The name of the group</param>
/// <param name="parentGroup">The parent group for this group to be under, if any</param>
/// <param name="order">The order in which to display the group</param>
/// <param name="platforms">The platform(s) the object should be displayed on</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DynamicFormGroupExpanderAttribute(
    DynamicFormLayout layout,
    string name = "",
    bool isExpanded = false,
    string? parentGroup = null,
    int order = 1000,
    string? visibleWhenTrue = null,
    DynamicFormPlatform platforms = DynamicFormPlatform.All) : DynamicFormGroupAttribute(layout, name, parentGroup, order, visibleWhenTrue, platforms)
{
    public override DynamicFormGroupStyle Style => DynamicFormGroupStyle.Expander;

    public bool IsExpanded => isExpanded;
}