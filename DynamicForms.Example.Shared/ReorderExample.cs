using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Example.Shared;

[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Second group", order: 2)]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "First group", order: 1)]
public class ReorderExample
{
    [DynamicFormFieldText(order: 2)] 
    public string TextOne => "This has an order of 2, so it should appear second.";
    
    [DynamicFormFieldText(order: 1)] 
    public string TextTwo => "This has an order of 1, so it should appear first.";
    
    [DynamicFormFieldText(order: 2)] 
    public string TextThree => "Things that have the same order should appear in the order from top to bottom. This also has an order of 2.";
    
    [DynamicFormFieldText] 
    public string TextFour => "Anything without an order will appear at the end.";
    
    [DynamicFormFieldText(groupName:"Second group")] 
    public string TextFiver => "Groups can have orders set as well.";
}