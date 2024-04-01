using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Example.Shared;

public class ChildObjectExample
{
    [DynamicFormObject] 
    public ChildObjectOtherObject ChildObjectOne { get; set; } = new();
    
    [DynamicFormObject] 
    public ChildObjectOtherObject ChildObjectTwo { get; set; } = new();
    
    [DynamicFormObject] 
    public ChildObjectOtherObjectGroups ChildObjectOtherObjectGroups { get; set; } = new();
}

public class ChildObjectOtherObject
{
    [DynamicFormFieldText]
    public string ChildObjectText { get; set; } = "By using the DynamicFormObject attribute, it'll look at properties within that object to add to the form. The same object can even be used multiple times.";
    
    [DynamicFormFieldTextBox(labelText: "Child Object Text Box")]
    public string ChildObjectTextBox { get; set; } = "";
}

[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Child Object with Groups")]
public class ChildObjectOtherObjectGroups
{
    [DynamicFormFieldText]
    public string ChildObjectText { get; set; } = "Child objects can even include groups.";
    
    [DynamicFormFieldTextBox(labelText: "Child Object Text Box")]
    public string ChildObjectTextBox { get; set; } = "";
}