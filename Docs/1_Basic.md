# Basic Usage

## 1. Create Projects

Create 3 projects:
- A shared class library for ViewModels
- A WPF application
- An Avalonia application

The WPF and Avalonia projects will then need to reference the shared class library.

## 2. Add Nuget Packages

Add the following nuget packages to your projects:

- Shared library: MattEqualsCoder.DynamicForms.Core
- WPF application: MattEqualsCoder.DynamicForms.WPF
- Avalonia application: MattEqualsCoder.DynamicForms.Avalonia

## 3. Create Shared ViewModel

In the shared class library, create a ViewModel with properties and events that you want to be in the form. Add attributes to the properties and events letting the library know what types of controls to use to represent the objects.

```
public class BasicViewModel
{
    [DynamicFormFieldText]
    public string BasicExampleText =>
        "DynamicForms allows you to use property and event attributes to build a form dynamically for both WPF and Avalonia.";
    
    [DynamicFormFieldTextBox(labelText: "Basic Text Box")]
    public string TextBoxOne { get; set; } = "";

    [DynamicFormFieldComboBox(labelText: "Basic Combo Box", comboBoxOptionsProperty: nameof(ComboBoxOptions))]
    public string ComboBoxOne { get; set; } = "Test 1";

    [DynamicFormFieldButton(buttonText: "View YAML")]
    public event EventHandler? ButtonPress;

    public string[] ComboBoxOptions => ["Test 1", "Test 2", "Test 3"];
}
```

## 4. Create Window

Create a window in the WPF and Avalonia projects. The following example code should work in both WPF and Avalonia:

**MainWindow.xaml/MainWindow.axaml**
```
<control:DynamicFormControl Data="{Binding}" Margin="5" />
```

**MainWindow.xaml.cs/MainWindow.axaml.cs**
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

![image](https://github.com/MattEqualsCoder/DynamicForms/assets/63823784/e681499c-beab-4562-8e46-cac52d18750e)
