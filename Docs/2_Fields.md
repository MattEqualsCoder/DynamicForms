# Fields

Below is an example of all fields available.

## Text Box

Basic input text box for string data.

```
[DynamicFormFieldTextBox(labelText: "DynamicFormFieldTextBox")]
public string DynamicFormFieldTextBoxExample { get; set; } = "Value of the text box";
```

## Text

Basic static text for displaying information to the user.

```
[DynamicFormFieldText]
public string DynamicFormFieldTextExample { get; set; } = "DynamicFormFieldText";
```

## Check Box

A check box for boolean values;

```
[DynamicFormFieldCheckBox("DynamicFormCheckBox", "DynamicFormCheckBox (label)")]
public bool DynamicFormFieldCheckBoxExample { get; set; }
```

## Combo Box

Two types of combo boxes can be created: string and enum combo boxes.

For enums, simply have the enum of as the data type for the property.

```
public enum EnumOptionExamples
{
    TestOne,
    TestTwo
}

[DynamicFormFieldComboBox("DynamicFormFieldComboBox (enum)")]
public EnumOptionExamples DynamicFormFieldComboBoxEnumExample { get; set; }
```

For string enums, you'll need another property for a collection of the combo box options, then specify the name of that property as the comboBoxOptionsProperty value;

```
public string[] DynamicFormFieldComboBoxOptions => ["Option 1", "Option 2", "Option 3"];

[DynamicFormFieldComboBox(comboBoxOptionsProperty: nameof(DynamicFormFieldComboBoxOptions), labelText: "DynamicFormFieldComboBox (string)")]
public string DynamicFormFieldComboBoxStringExample { get; set; } = "Option 1";

```

## Sliders

This control is for numbers with a horizontal slider for dragging to set the value. To the left of the slider is a text block with the current value of the slider. Can be used for ints, doubles, floats, and decimals.

```
[DynamicFormFieldSlider(1, 10, labelText: "DynamicFormFieldSlider (int)")]
public int DynamicFormFieldSliderIntExample { get; set; } = 1;

[DynamicFormFieldSlider(0, 100, decimalPlaces: 2, incrementAmount: 0.25, suffix: "%", labelText: "DynamicFormFieldSlider (double)")]
public double DynamicFormFieldDoubleSlider { get; set; } = 1;
```

## Color Picker

Currently this doesn't have a popup for picking a color, but it can be used to allow the user to enter a hexidecimal ARGB color value which is converted to a byte array. There is a preview box that displays the color entered.

```
[DynamicFormFieldColorPicker("DynamicFormFieldColorPicker")]
public byte[] DynamicFormFieldColorPickerExample { get; set; } = [0xFF, 0x21, 0x21, 0x21];
```

## File & Folder Picker

A control with a button to click to select a file or folder. Supports filters and validating the selected file against a specific checksum.

```
[DynamicFormFieldFilePicker(FilePickerType.OpenFile, "Text Files (*.txt, *.log)|*.txt;*.log|All files (*.*)|*.*", "", "", "DynamicFormFieldFilePicker (files)")]
public string DynamicFormFieldFilePickerFileExample { get; set; } = "";

[DynamicFormFieldFilePicker(FilePickerType.Folder)]
public string DynamicFormFieldFilePickerFolderExample { get; set; } = "";
```

## Numeric Up Down

A control with a text box for numbers and buttons to increment/decrement the value.

```
[DynamicFormFieldNumericUpDown(increment: 5, minValue: 0, maxValue: 50, labelText: "DynamicFormFieldNumericUpDownExample")]
public int DynamicFormFieldNumericUpDownExample { get; set; }
```

## Enable Disable Reorder

A control for a collection of strings that allows the user to enable/disable strings and organize them from top to bottom.

```
public string[] DynamicFormFieldEnableDisableReorderOptions => ["One", "Two", "Three", "Four", "Five"];

[DynamicFormFieldEnableDisableReorder(nameof(DynamicFormFieldEnableDisableReorderOptions), "DynamicFormFieldEnableDisableReorder")]
public string[] DynamicFormFieldEnableDisableReorderExample { get; set; } = ["Five", "Two", "One"];
```

## Buttons

For events, you can have buttons which will invoke them.

```
[DynamicFormFieldButton("View YAML")] 
public event EventHandler? ButtonPress;
```