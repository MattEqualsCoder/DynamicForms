using System.Windows;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DynamicForms.Example.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
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
            BasicTextBox.Text = _serializer.Serialize(_viewModel.BasicExample);
        };
        
        _viewModel.FieldsExample.ButtonPress += (sender, args) =>
        {
            FieldsTextBox.Text = _serializer.Serialize(_viewModel.FieldsExample);
        };
    }
}