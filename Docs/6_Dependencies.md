# Dependencies

Sometimes you may want to have fields that will only be visible or editable in certain circumstances. This can be achieved by having the ViewModel implement the INotifyPropertyChanged interface. You can then use boolean properties to set the visibility and editable states of other fields automatically when they change. For fields that you want to be dependent on other fields, you can set the attribute parameters visibleWhenProperty and editableWhenProperty to the name of the properties to monitor.

## Changing Visibility
```
public class DependencyExample : INotifyPropertyChanged
{
    private bool _checkBox;

    [DynamicFormFieldCheckBox(checkBoxText: "Check this to make the text box visible")]
    public bool CheckBox
    {
        get => _checkBox;
        set => SetField(ref _checkBox, value);
    }

    [DynamicFormFieldTextBox(labelText: "Text Box", visibleWhenProperty: nameof(CheckBox))]
    public string TextBox { get; set; } = "";
}
```

## Changing Editable
```
public class DependencyExample : INotifyPropertyChanged
{
    private bool _checkBox;

    [DynamicFormFieldCheckBox(checkBoxText: "Check this to make the text box editable")]
    public bool CheckBox
    {
        get => _checkBox;
        set => SetField(ref _checkBox, value);
    }

    [DynamicFormFieldTextBox(labelText: "Text Box", editableWhenProperty: nameof(CheckBox))]
    public string TextBox { get; set; } = "";
}
```