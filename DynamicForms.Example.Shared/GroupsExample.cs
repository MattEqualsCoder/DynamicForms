using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Example.Shared;

[DynamicFormGroupBasic(DynamicFormLayout.Vertical, "Basic (name not displayed)")]
[DynamicFormGroupGroupBox(DynamicFormLayout.SideBySide, "Group Box with Side-By-Side Type")]
[DynamicFormGroupExpander(DynamicFormLayout.TwoColumns, "Expander with Two Columns Type")]
[DynamicFormGroupExpander(DynamicFormLayout.TwoColumns, "Expander with Child Groups")]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Left Box", parentGroup: "Expander with Child Groups", platforms: DynamicFormPlatform.Linux | DynamicFormPlatform.Windows)]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Right Box", parentGroup: "Expander with Child Groups")]
public class GroupsExample
{
    [DynamicFormFieldText(label: "Basic Text")]
    public string BasicText => "Basic groups don't have any special formatting. Objects without a group name are automatically added to the first object. Vertical types will go up-to-down with the label above the field.";
    
    [DynamicFormFieldTextBox(label: "Basic Text Box")]
    public string BasicTextBox { get; set; } = "";
    
    [DynamicFormFieldText(label: "Side-by-side", groupName: "Group Box with Side-By-Side Type")]
    public string InnerGroupBoxText => "Group boxes will have a border with the name at the top. Side-by-side types will have the label on the left and the field to the right. Use Grid.IsSharedSizeScope=\"True\" to have the widths be consistent. To add a field to a group, set its groupName attribute parameter.";

    [DynamicFormFieldTextBox(label: "Text Box", groupName: "Group Box with Side-By-Side Type")]
    public string InnerGroupBoxTextBox { get; set; } = "";
    
    [DynamicFormFieldText(label: "Two Column", groupName: "Expander with Two Columns Type")]
    public string ExpanderText => "Expander boxes can be collapsed/expanded to show and hide the content. Two column will have fields in two columns with the label appearing above the field.";

    [DynamicFormFieldTextBox(label: "Text Box", groupName: "Expander with Two Columns Type")]
    public string ExpanderTextBox { get; set; } = "";
    
    [DynamicFormFieldText(groupName: "Left Box")]
    public string LeftTextBox { get; set; } = "Groups can actually exist within another group by specifying the parentGroup attribute parameter.";
    
    [DynamicFormFieldTextBox(label: "Text Box", groupName: "Right Box")]
    public string RightTextBox { get; set; } = "";
}