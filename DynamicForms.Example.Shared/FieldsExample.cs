using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using YamlDotNet.Serialization;

namespace DynamicForms.Example.Shared;

[DynamicFormGroupBasic(DynamicFormLayout.SideBySide)]
public class FieldsExample
{
    [DynamicFormFieldTextBox("DynamicFormFieldTextBox")]
    public string? DynamicFormFieldTextBoxExample { get; set; }

    [DynamicFormFieldText(label: "DynamicFormFieldText (label)")]
    [YamlIgnore]
    public string DynamicFormFieldTextExample { get; set; } = "DynamicFormFieldText";
    
    [DynamicFormFieldText(DynamicFormAlignment.Right, label: "DynamicFormFieldText (label)")]
    [YamlIgnore]
    public string DynamicFormFieldTextRightAlignedExample { get; set; } = "Right Aligned Text";
    
    [DynamicFormFieldCheckBox("DynamicFormCheckBox", DynamicFormAlignment.Right,"DynamicFormCheckBox (label)")]
    public bool? DynamicFormFieldCheckBoxExample { get; set; }

    [DynamicFormFieldComboBox(comboBoxOptionsProperty: nameof(DynamicFormFieldComboBoxOptions), 
        label: "DynamicFormFieldComboBox (string)")]
    public string? DynamicFormFieldComboBoxStringExample { get; set; }
    
    [DynamicFormFieldComboBox("DynamicFormFieldComboBox (enum)")]
    public EnumOptionExamples DynamicFormFieldComboBoxEnumExample { get; set; }

    [DynamicFormFieldComboBox(comboBoxOptionsProperty: nameof(DynamicFormFieldComboBoxDictionaryOptions),
        label: "DynamicFormFieldComboBox (dictionary)")]
    public string? DynamicFormFieldComboBoxDictionaryExample { get; set; } = "Value 2";

    [DynamicFormFieldSlider(1, 10, label: "DynamicFormFieldSlider (int)")]
    public int? DynamicFormFieldSliderIntExample { get; set; } = 5;

    [DynamicFormFieldSlider(0, 100, decimalPlaces: 2, incrementAmount: 0.25, suffix: "%",
        label: "DynamicFormFieldSlider (double)")]
    public double? DynamicFormFieldDoubleSlider { get; set; } = 25.1;
    
    [DynamicFormFieldColorPicker("DynamicFormFieldColorPicker")]
    public byte[]? DynamicFormFieldColorPickerExample { get; set; }
    
    [DynamicFormFieldFilePicker(FilePickerType.OpenFile, "Text Files (*.txt, *.log)|*.txt;*.log|All files (*.*)|*.*", dialogText: "Select text file", label: "DynamicFormFieldFilePicker (files)")]
    public string? DynamicFormFieldFilePickerFileExample { get; set; }
    
    [DynamicFormFieldFilePicker(FilePickerType.Folder, dialogText: "Select folder", label: "DynamicFormFieldFilePicker (folder)")]
    public string? DynamicFormFieldFilePickerFolderExample { get; set; }
    
    [DynamicFormFieldNumericUpDown(label: "DynamicFormFieldNumericUpDownExample")]
    public int? DynamicFormFieldNumericUpDownExample { get; set; }
    
    [DynamicFormFieldNumericUpDown(increment: 0.1, label: "DynamicFormFieldNumericUpDownExample (decimal)")]
    public decimal? DynamicFormFieldNumericUpDownExampleDouble { get; set; }
    
    [DynamicFormFieldEnableDisableReorder(nameof(DynamicFormFieldEnableDisableReorderOptions), "DynamicFormFieldEnableDisableReorder")]
    public string[]? DynamicFormFieldEnableDisableReorderExample { get; set; }
    
    [DynamicFormFieldButton("View YAML")] 
#pragma warning disable CS0067 // Event is never used
    public event EventHandler? ButtonPress;
#pragma warning restore CS0067 // Event is never used

    [YamlIgnore]
    public string[] DynamicFormFieldComboBoxOptions => ["Option 1", "Option 2", "Option 3"];

    [YamlIgnore] public Dictionary<string, string> DynamicFormFieldComboBoxDictionaryOptions =>
        new Dictionary<string, string>()
        {
            { "Value 1", "Display 1" },
            { "Value 2", "Display 2" },
            { "Value 3", "Display 3" },
        };

    [YamlIgnore]
    public string[] DynamicFormFieldEnableDisableReorderOptions => ["One", "Two", "Three", "Four", "Five"];
}