using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using YamlDotNet.Serialization;

namespace DynamicForms.Example.Shared;

[DynamicFormGroupBasic(DynamicFormGroupType.SideBySide)]
public class FieldsExample
{
    [DynamicFormFieldTextBox("DynamicFormFieldTextBox")]
    public string DynamicFormFieldTextBoxExample { get; set; } = "";

    [DynamicFormFieldText("DynamicFormFieldText (label)")]
    [YamlIgnore]
    public string DynamicFormFieldTextExample { get; set; } = "DynamicFormFieldText";
    
    [DynamicFormFieldCheckBox("DynamicFormCheckBox", "DynamicFormCheckBox (label)")]
    public bool DynamicFormFieldCheckBoxExample { get; set; }

    [DynamicFormFieldComboBox("DynamicFormFieldComboBox (string)",
        comboBoxOptionsProperty: nameof(DynamicFormFieldComboBoxOptions))]
    public string DynamicFormFieldComboBoxStringExample { get; set; } = "Option 1";
    
    [DynamicFormFieldComboBox("DynamicFormFieldComboBox (enum)")]
    public EnumOptionExamples DynamicFormFieldComboBoxEnumExample { get; set; }
    
    [DynamicFormFieldSlider(1, 10, "DynamicFormFieldSlider (int)")]
    public int DynamicFormFieldSliderIntExample { get; set; } = 1;
    
    [DynamicFormFieldSlider(0, 100, "DynamicFormFieldSlider (double)", decimalPlaces: 2, incrementAmount: 0.25, suffix: "%")]
    public double DynamicFormFieldDoubleSlider { get; set; } = 1;
    
    [DynamicFormFieldColorPicker("DynamicFormFieldColorPicker")]
    public byte[] DynamicFormFieldColorPickerExample { get; set; } = [0xFF, 0x21, 0x21, 0x21];
    
    [DynamicFormFieldFilePicker(FilePickerType.OpenFile, "Text Files (*.txt, *.log)|*.txt;*.log|All files (*.*)|*.*", "", "", "DynamicFormFieldFilePicker (files)")]
    public string DynamicFormFieldFilePickerFileExample { get; set; } = "";
    
    [DynamicFormFieldFilePicker(FilePickerType.Folder)]
    public string DynamicFormFieldFilePickerFolderExample { get; set; } = "";
    
    [DynamicFormFieldNumericUpDown(labelText: "DynamicFormFieldNumericUpDown")]
    public int DynamicFormFieldNumericUpDownExample { get; set; }
    
    [DynamicFormFieldEnableDisableReorder(nameof(DynamicFormFieldEnableDisableReorderOptions), "DynamicFormFieldEnableDisableReorder")]
    public string[] DynamicFormFieldEnableDisableReorderExample { get; set; } = ["Five", "Two", "One"];
    
    [DynamicFormFieldButton("View YAML")] 
    public event EventHandler? ButtonPress;

    [YamlIgnore]
    public string[] DynamicFormFieldComboBoxOptions => ["Option 1", "Option 2", "Option 3"];
    
    [YamlIgnore]
    public string[] DynamicFormFieldEnableDisableReorderOptions => ["One", "Two", "Three", "Four", "Five"];
}