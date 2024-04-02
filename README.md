# Dynamic Forms
Dynamic UI builder for WPF and Avalonia. By creating a ViewModel with attributes attached to properties and events and passing that ViewModel into the DynamicFormControl, you can have the control build a form with various different controls to accommodate a wide variety of data types.

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/8299a2d7-c47e-45c3-82b6-d6fae0149c83)

You can create the above via the below code:

**ViewModel**
```
public class BasicViewModel
{
    [DynamicFormFieldText]
    public string BasicExampleText => "DynamicForms allows you to use property and event attributes to build a form dynamically for both WPF and Avalonia.";
    
    [DynamicFormFieldTextBox("Basic Text Box")]
    public string TextBoxOne { get; set; } = "";

    [DynamicFormFieldComboBox("Basic Combo Box", comboBoxOptionsProperty: nameof(ComboBoxOptions))]
    public string ComboBoxOne { get; set; } = "Test 3";

    [DynamicFormFieldButton("View YAML")] 
    public event EventHandler? ButtonPress;

    public string[] ComboBoxOptions => ["Test 1", "Test 2", "Test 3"];
}
```

**Window C#**
```
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var model = new BasicViewModel();

        model.ButtonPress += (sender, args) =>
        {
            Console.WriteLine("Button pressed");
        };

        DataContext = model;
    }
}
```

**Window XAML**
```
<control:DynamicFormControl Data="{Binding}" Margin="5" />
```

## Usage

1. Create multiple projects
    - Shared library for view model classes
    - WPF application
    - Avalonia application
2. Install nuget packages
    - Shared library: MattEqualsCoder.DynamicForms.Core
    - WPF application: MattEqualsCoder.DynamicForms.WPF
    - Avalonia application: MattEqualsCoder.DynamicForms.Avalonia
3. Add controls to WPF and Avalonia windows

## Documentation

You can view additional documentation about using it [here](https://github.com/MattEqualsCoder/DynamicForms/blob/main/Docs/1_Basic.md).
