using DynamicForms.Library.Core.Attributes;
using YamlDotNet.Serialization;

namespace DynamicForms.Example.Shared;

public class BasicExample
{
    [DynamicFormFieldTextBox("Basic Text Box")]
    public string TextBoxOne { get; set; } = "";

    [DynamicFormFieldComboBox("Basic Combo Box", comboBoxOptionsProperty: nameof(ComboBoxOptions))]
    public string ComboBoxOne { get; set; } = "Test 3";

    [DynamicFormFieldButton("View YAML")] 
    public event EventHandler? ButtonPress;

    [YamlIgnore]
    public string[] ComboBoxOptions => ["Test 1", "Test 2", "Test 3"];
}