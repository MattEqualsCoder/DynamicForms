using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Example.Shared;

public class DependencyExample : INotifyPropertyChanged
{
    private bool _checkBoxOne = true;
    private bool _checkBoxTwo = true;
    private string _nameTextBox = "";
    private string _nameText = "Please enter a name above";
    
    [DynamicFormFieldText]
    public string DependencyExampleText =>
        "If the ViewModel implements INotifyPropertyChanged, then you can have fields be visible/editable based on other properties.";

    [DynamicFormFieldCheckBox(checkBoxText: "Make first text box visible")]
    public bool CheckBoxOne
    {
        get => _checkBoxOne;
        set => SetField(ref _checkBoxOne, value);
    }

    [DynamicFormFieldTextBox(labelText: "First Text Box", visibleWhenTrue: nameof(CheckBoxOne))]
    public string TextBoxOne { get; set; } = "";
    
    [DynamicFormFieldCheckBox(checkBoxText: "Make second text box editable")]
    public bool CheckBoxTwo
    {
        get => _checkBoxTwo;
        set => SetField(ref _checkBoxTwo, value);
    }

    [DynamicFormFieldTextBox(labelText: "Second Text Box", editableWhenTrue: nameof(CheckBoxTwo))]
    public string TextBoxTwo { get; set; } = "";

    [DynamicFormFieldTextBox(labelText: "Enter a Name")]
    public string NameTextBox
    {
        get => _nameTextBox;
        set
        {
            SetField(ref _nameTextBox, value);
            NameDisplayText = string.IsNullOrEmpty(value) ? "Please enter a name above" : $"Hello {value}";
        }
    }
    
    [DynamicFormFieldText]
    public string NameDisplayText
    {
        get => _nameText;
        set => SetField(ref _nameText, value);
    }

    [DynamicFormFieldButton("Submit Name")]
    public DependencyCommand DependencyCommand => new DependencyCommand();

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

public class DependencyCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        if (parameter is not DependencyExample example)
        {
            return false;
        }

        return !string.IsNullOrEmpty(example.NameTextBox);
    }

    public void Execute(object? parameter)
    {
        if (parameter is not DependencyExample example)
        {
            return;
        }

        example.NameTextBox = "";
    }

    public event EventHandler? CanExecuteChanged;
}