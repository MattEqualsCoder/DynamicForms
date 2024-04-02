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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/8498dcf5-27d7-4fcf-99c5-b53cee1920ca)

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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/d19adde4-c43f-4d95-86f5-7bc2c9bcb522)
