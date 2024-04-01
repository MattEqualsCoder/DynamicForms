using DynamicForms.Library.Core.Attributes;
using YamlDotNet.Serialization;

namespace DynamicForms.Example.Shared;

public class BasicExample
{
    [DynamicFormFieldText]
    public string BasicExampleText =>
        "DynamicForms allows you to use property and event attributes to build a form dynamically for both WPF and Avalonia.";
    
    [DynamicFormFieldTextBox(labelText: "Basic Text Box")]
    public string TextBoxOne { get; set; } = "";

    [DynamicFormFieldComboBox(labelText: "Basic Combo Box", comboBoxOptionsProperty: nameof(ComboBoxOptions))]
    public string ComboBoxOne { get; set; } = "Test 3";

    [DynamicFormFieldButton(buttonText: "View YAML")]
    public event EventHandler? ButtonPress;

    public string[] ComboBoxOptions => ["Test 1", "Test 2", "Test 3"];
}