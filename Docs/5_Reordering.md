# Reordering

By default fields are displayed in the form in the order that the properties appear within the class. However, you can specify the order of both fields and groups by setting the order attribute parameter. The default order value is the max integer value, so any fields or groups where an order is specified will appear above others that do not specify an order value.

## Reordering Fields
```
public class ReorderExample
{
    [DynamicFormFieldText(order: 2)] 
    public string TextOne => "This will appear second.";
    
    [DynamicFormFieldText(order: 1)] 
    public string TextTwo => "This will appear first.";
    
    [DynamicFormFieldText(order: 3)] 
    public string TextThree => "This will appear third.";
}
```

## Reordering Groups
```
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Second group", order: 2)]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "First group", order: 1)]
public class ReorderExample
{
    [DynamicFormFieldText(groupName: "First group)] 
    public string TextOne => "This will appear in the first group.";
    
    [DynamicFormFieldText(groupName: "Second group")] 
    public string TextTwo => "This will appear in the second group.";
}
```