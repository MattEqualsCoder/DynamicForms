# Groups

Within each form, you can organize your fields into groups, and you can even have groups within groups. Each field you have in the form can then be assigned to a group based on name.

## Basics
To start off, you first initialize your group(s) as properties attached to the class.

```
[DynamicFormGroupBasic(DynamicFormLayout.Vertical, "Group One")]
[DynamicFormGroupBasic(DynamicFormLayout.Vertical, "Group Two")]
public class GroupsExample
{
}
```

Then, when you're creating properties for fields in the form, you can set the groupName parameter in the attributes to assign them to a group. If no groupName is specified, it'll default to the first group.

```
[DynamicFormFieldText(labelText: "Basic Text Box One", groupName: "Group One")]
public string BasicTextOne => "Text that will display in group one";

[DynamicFormFieldText(labelText: "Basic Text Box Two", groupName: "Group two")]
public string BasicTextTwo => "Text that will display in group two";
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/9e28a11a-b68a-413f-929a-8abf18d81884)

## Group Types
There are different types of groups that can be used that affect the style of the group.

**DynamicFormGroupBasic**: Displays the fields with no special formatting or styling.
**DynamicFormGroupGroupBox**: Displays the fields with a border and header text.
**DynamicFormGroupExpander**: Displays the fields in a section that can be collapsed & restored.

```
[DynamicFormGroupBasic(DynamicFormLayout.Vertical, "Group One")]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Group Two")]
[DynamicFormGroupExpander(DynamicFormLayout.Vertical, "Group Three")]
public class GroupsExample
{
    [DynamicFormFieldText(groupName: "Group One")]
    public string BasicText => "This is a basic group.";

    [DynamicFormFieldText(groupName: "Group Two")]
    public string GroupBoxText => "This is a group box group.";

    [DynamicFormFieldText(groupName: "Group Three")]
    public string ExpanderText => "This is an expander group.";
}
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/6eb5d100-31a7-4169-8a03-16f59f4434dd)

## Group Layouts
There are also different layouts that affect the structure of the fields within a group.

**DynamicFormLayout.Vertical**: Displays the fields vertically with the label above the field's control.
**DynamicFormLayout.SideBySide**: Displays the label to the left of the field control. You can set Grid.IsSharedSizeScope="True" to have the fields align.
**DynamicFormLayout.TwoColumns**: Displays the fields in two columns, with the labels above the field's control.

```
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Group One")]
[DynamicFormGroupGroupBox(DynamicFormLayout.SideBySide, "Group Two")]
[DynamicFormGroupGroupBox(DynamicFormLayout.TwoColumns, "Group Three")]
public class GroupsExample
{
    [DynamicFormFieldTextBox(labelText: "Group One Text Box One", groupName: "Group One")]
    public string GroupOneTextBoxOne { get; set; } = "";

    [DynamicFormFieldTextBox(labelText: "Group One Text Box Two", groupName: "Group One")]
    public string GroupOneTextBoxTwo { get; set; } = "";

    [DynamicFormFieldTextBox(labelText: "Group Two Text Box One", groupName: "Group Two")]
    public string GroupTwoTextBoxOne { get; set; } = "";

    [DynamicFormFieldTextBox(labelText: "Group Two Text Box Two", groupName: "Group Two")]
    public string GroupTwoTextBoxTwo { get; set; } = "";

    [DynamicFormFieldTextBox(labelText: "Group Three Text Box One", groupName: "Group Three")]
    public string GroupThreeTextBoxOne { get; set; } = "";

    [DynamicFormFieldTextBox(labelText: "Group Three Text Box Two", groupName: "Group Three")]
    public string GroupThreeTextBoxTwo { get; set; } = "";
}
```

```
<control:DynamicFormControl Grid.IsSharedSizeScope="True" Data="{Binding}" Margin="5" />
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/269d5e4d-acce-46fa-8719-1acdffdb961f)

## Subgroups
Groups can be placed within other groups by setting the parentGroup attribute parameter.

```
[DynamicFormGroupBasic(DynamicFormLayout.TwoColumns, "Parent Group")]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Left Box", parentGroup: "Parent Group")]
[DynamicFormGroupGroupBox(DynamicFormLayout.Vertical, "Right Box", parentGroup: "Parent Group")]
public class GroupsExample
{
    [DynamicFormFieldTextBox(labelText: "Left Text Box",groupName: "Left Box")]
    public string LeftTextBox { get; set; } = "";
    
    [DynamicFormFieldTextBox(labelText: "Right Text Box", groupName: "Right Box")]
    public string RightTextBox { get; set; } = "";
}
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/1fa5ca14-68a4-4c16-92f1-e68316bfccab)
