using System.ComponentModel;
using System.Runtime.CompilerServices;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Example.Shared;

public class DependencyExample : INotifyPropertyChanged
{
    private bool _checkBoxOne;
    private bool _checkBoxTwo;
    
    [DynamicFormFieldText]
    public string DependencyExampleText =>
        "If the ViewModel implements INotifyPropertyChanged, then you can have fields be visible/editable based on other properties.";

    [DynamicFormFieldCheckBox(checkBoxText: "Make first text box visible")]
    public bool CheckBoxOne
    {
        get => _checkBoxOne;
        set => SetField(ref _checkBoxOne, value);
    }

    [DynamicFormFieldTextBox(labelText: "First Text Box", visibleWhenProperty: nameof(CheckBoxOne))]
    public string TextBoxOne { get; set; } = "";
    
    [DynamicFormFieldCheckBox(checkBoxText: "Make first text box editable")]
    public bool CheckBoxTwo
    {
        get => _checkBoxTwo;
        set => SetField(ref _checkBoxTwo, value);
    }

    [DynamicFormFieldTextBox(labelText: "Second Text Box", editableWhenProperty: nameof(CheckBoxTwo))]
    public string TextBoxTwo { get; set; } = "";
    
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