# DynamicForms
Dynamic UI builder for WPF and Avalonia. By creating a ViewModel with attributes attached to properties and events and passing that ViewModel into the DynamicFormControl, you can have the control build a form with various different controls to accommodate a wide variety of data types.

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
