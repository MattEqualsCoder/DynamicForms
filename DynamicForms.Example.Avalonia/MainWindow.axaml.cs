using Avalonia.Controls;
using DynamicForms.Example.Avalonia.ViewModels;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DynamicForms.Example.Avalonia;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;
    
    private ISerializer _serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = _viewModel = new MainWindowViewModel();

        _viewModel.BasicExample.ButtonPress += (sender, args) =>
        {
            this.Find<TextBox>(nameof(BasicTextBox))!.Text = _serializer.Serialize(_viewModel.BasicExample);
        };
        
        _viewModel.FieldsExample.ButtonPress += (sender, args) =>
        {
            this.Find<TextBox>(nameof(FieldsTextBox))!.Text = _serializer.Serialize(_viewModel.FieldsExample);
        };

        _viewModel.FieldsExample.RefreshReorderBox += (sender, args) =>
        {
            _viewModel.FieldsExample.DynamicFormFieldEnableDisableReorderOptions = ["Test1", "Test2"];
        };
    }
}