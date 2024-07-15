using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormDictionaryComboBox : UserControl
{
    private ComboBox _comboBox;
    private Dictionary<string, string> _items;

    public DynamicFormDictionaryComboBox() : this("", [])
    {

    }

    public DynamicFormDictionaryComboBox(string? value, Dictionary<string, string> items)
    {
        _items = items;
        InitializeComponent();
        DataContext = new DynamicFormDictionaryComboBoxViewModel()
        {
            Items = items
        };
        _comboBox = this.Find<ComboBox>(nameof(MainControl))!;
        _comboBox.SelectionChanged += (sender, args) =>
        {
            OnValueChanged?.Invoke(this, EventArgs.Empty);
        };
        SetValue(value);
    }
    
    public void SetValue(string? value)
    {
        var item = _items.FirstOrDefault(x => x.Key == value);
        _comboBox.SelectedItem = item;
    }
    
    public string? GetValue()
    {
        var currentPair = _comboBox.SelectedItem as KeyValuePair<string, string>?;
        return currentPair?.Key;
    }

    public event EventHandler? OnValueChanged;
}

public class DynamicFormDictionaryComboBoxViewModel
{
    public Dictionary<string, string> Items { get; set; } = new();
}