# Child Objects

Not only can basic values be placed within a form, but objects can be as well. Those objects can have their own properties and groups which are then added to the main form.

To specify a property as an object that should be scanned, use the DynamicFormObject attribute.

```
public class ChildObjectOtherObject
{
    [DynamicFormFieldTextBox(labelText: "Child Object Text Box")]
    public string ChildObjectTextBox { get; set; } = "";
}

public class ChildObjectExample
{
    [DynamicFormObject] 
    public ChildObjectOtherObject ChildObjectOne { get; set; } = new();
}
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/02aed944-1e19-4851-b221-7ca74143015e)

You can also specify groups for the class that's used as a child object.

```
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Child Object with Groups")]
public class ChildObjectOtherObjectGroups
{
    [DynamicFormFieldTextBox(labelText: "Child Object Text Box")]
    public string ChildObjectTextBox { get; set; } = "";
}

public class ChildObjectExample
{
    [DynamicFormObject] 
    public ChildObjectOtherObjectGroups ChildObjectOtherObjectGroups { get; set; } = new();
}
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/1324697b-b41e-4ba2-8e21-af7334ceb2fc)
