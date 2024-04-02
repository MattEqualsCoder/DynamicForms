# Fields

Below is an example of all fields available.

## Text Box

Basic input text box for string data.

```
[DynamicFormFieldTextBox(labelText: "DynamicFormFieldTextBox")]
public string DynamicFormFieldTextBoxExample { get; set; } = "Value of the text box";
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/73f71eaf-ea22-4620-a928-52628bf64361)

## Text

Basic static text for displaying information to the user.

```
[DynamicFormFieldText]
public string DynamicFormFieldTextExample { get; set; } = "DynamicFormFieldText";
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/18e7e182-18d0-4c30-9e41-caf0cef30d6b)


## Check Box

A check box for boolean values;

```
[DynamicFormFieldCheckBox("DynamicFormCheckBox", "DynamicFormCheckBox (label)")]
public bool DynamicFormFieldCheckBoxExample { get; set; }
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/d3a27913-b4dd-4fa6-b967-ef835123f2e5)


## Combo Box

Two types of combo boxes can be created: string and enum combo boxes.

For enums, simply have the enum of as the data type for the property.

```
public enum EnumOptionExamples
{
    TestOne,
    TestTwo
}

[DynamicFormFieldComboBox(labelText: "DynamicFormFieldComboBox (enum)")]
public EnumOptionExamples DynamicFormFieldComboBoxEnumExample { get; set; }
```

For string enums, you'll need another property for a collection of the combo box options, then specify the name of that property as the comboBoxOptionsProperty value;

```
public string[] DynamicFormFieldComboBoxOptions => ["Option 1", "Option 2", "Option 3"];

[DynamicFormFieldComboBox(comboBoxOptionsProperty: nameof(DynamicFormFieldComboBoxOptions), labelText: "DynamicFormFieldComboBox (string)")]
public string DynamicFormFieldComboBoxStringExample { get; set; } = "Option 1";

```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/e5b9965e-1ad8-4e13-9dc0-03b8f050f5d7)


## Sliders

This control is for numbers with a horizontal slider for dragging to set the value. To the left of the slider is a text block with the current value of the slider. Can be used for ints, doubles, floats, and decimals.

```
[DynamicFormFieldSlider(1, 10, labelText: "DynamicFormFieldSlider (int)")]
public int DynamicFormFieldSliderIntExample { get; set; } = 1;

[DynamicFormFieldSlider(0, 100, decimalPlaces: 2, incrementAmount: 0.25, suffix: "%", labelText: "DynamicFormFieldSlider (double)")]
public double DynamicFormFieldDoubleSlider { get; set; } = 1;
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/c55d997c-2c96-403a-a7ba-f51f36f0daf9)


## Color Picker

Currently this doesn't have a popup for picking a color, but it can be used to allow the user to enter a hexidecimal ARGB color value which is converted to a byte array. There is a preview box that displays the color entered.

```
[DynamicFormFieldColorPicker("DynamicFormFieldColorPicker")]
public byte[] DynamicFormFieldColorPickerExample { get; set; } = [0xFF, 0x21, 0x21, 0x21];
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/43bead50-1fa1-4082-97de-cb21c38efe89)

## File & Folder Picker

A control with a button to click to select a file or folder. Supports filters and validating the selected file against a specific checksum.

```
[DynamicFormFieldFilePicker(FilePickerType.OpenFile, "Text Files (*.txt, *.log)|*.txt;*.log|All files (*.*)|*.*", "", "", "DynamicFormFieldFilePicker (files)")]
public string DynamicFormFieldFilePickerFileExample { get; set; } = "";

[DynamicFormFieldFilePicker(FilePickerType.Folder)]
public string DynamicFormFieldFilePickerFolderExample { get; set; } = "";
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/fce74feb-7bc5-4cc5-96a4-456e69365eb0)

## Numeric Up Down

A control with a text box for numbers and buttons to increment/decrement the value.

```
[DynamicFormFieldNumericUpDown(increment: 5, minValue: 0, maxValue: 50, labelText: "DynamicFormFieldNumericUpDownExample")]
public int DynamicFormFieldNumericUpDownExample { get; set; }
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/ad95507e-d71d-4af4-bdd6-cf63fa300196)

## Enable Disable Reorder

A control for a collection of strings that allows the user to enable/disable strings and organize them from top to bottom.

```
public string[] DynamicFormFieldEnableDisableReorderOptions => ["One", "Two", "Three", "Four", "Five"];

[DynamicFormFieldEnableDisableReorder(nameof(DynamicFormFieldEnableDisableReorderOptions), "DynamicFormFieldEnableDisableReorder")]
public string[] DynamicFormFieldEnableDisableReorderExample { get; set; } = ["Five", "Two", "One"];
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/0a8a2edb-77b1-4d93-8f54-fb82406ea34e)

## Buttons

For events, you can have buttons which will invoke them.

```
[DynamicFormFieldButton("Button Text")] 
public event EventHandler? ButtonPress;
```

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/fdf9d0aa-aa4c-44dc-9be9-d6d4153fc10f)
