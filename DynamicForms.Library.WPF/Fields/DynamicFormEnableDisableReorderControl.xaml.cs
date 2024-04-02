using System.Windows;
using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormEnableDisableReorderControl : UserControl
{
    private readonly bool _isArray;
    private readonly ICollection<string> _options;
    private ICollection<string> _selectedOptions;
    
    public DynamicFormEnableDisableReorderControl(ICollection<string> options, ICollection<string> selectedOptions, Type type)
    {
        InitializeComponent();
        
        if (type == typeof(string[]))
        {
            _isArray = true;
        }

        _options = options;
        _selectedOptions = selectedOptions;
        PopulateListBox();
    }
    
    public object GetValue()
    {
        if (_isArray)
        {
            return _selectedOptions.ToArray();
        }
        else
        {
            return _selectedOptions.ToList();
        }
    }

    public void SetValue(ICollection<string> selectedOptions)
    {
        _selectedOptions = selectedOptions;
        PopulateListBox();
    }

    public event EventHandler? ValueUpdated;

    private void PopulateListBox()
    {
        MainListBox.Items.Clear();

        foreach (var item in _selectedOptions)
        {
            var itemControl = new DynamicFormEnableDisableReorderControlItem(true, item,
                item != _selectedOptions.First(), item != _selectedOptions.Last());
            itemControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            MainListBox.Items.Add(new ListBoxItem() { Content = itemControl, HorizontalContentAlignment = HorizontalAlignment.Stretch });
            itemControl.OnCheckChanged += ItemControlOnOnCheckChanged;
        }
        
        foreach (var item in _options.Where(x => !_selectedOptions.Contains(x)))
        {
            var itemControl = new DynamicFormEnableDisableReorderControlItem(false, item, false, false);
            MainListBox.Items.Add(new ListBoxItem() { Content = itemControl, HorizontalContentAlignment = HorizontalAlignment.Stretch });
            itemControl.OnCheckChanged += ItemControlOnOnCheckChanged;
        }
    }

    private void ItemControlOnOnCheckChanged(object? sender, EventArgs e)
    {
        if (sender is not DynamicFormEnableDisableReorderControlItem item)
        {
            return;
        }

        if (item.IsCheckChanged)
        {
            if (item.IsOptionEnabled && !_selectedOptions.Contains(item.OptionName))
            {
                _selectedOptions = _selectedOptions.Append(item.OptionName).ToList();
            }
            else if (!item.IsOptionEnabled && _selectedOptions.Contains(item.OptionName))
            {
                _selectedOptions = _selectedOptions.Where(x => x != item.OptionName).ToList();
            }
        }
        else if (item.MoveUp)
        {
            var newList = _selectedOptions.ToList();
            var index = newList.IndexOf(item.OptionName) - 1;
            newList.Remove(item.OptionName);
            newList.Insert(index, item.OptionName);
            _selectedOptions = newList;
        }
        else if (item.MoveDown)
        {
            var newList = _selectedOptions.ToList();
            var index = newList.IndexOf(item.OptionName) + 1;
            newList.Remove(item.OptionName);
            newList.Insert(index, item.OptionName);
            _selectedOptions = newList;
        }
        
        Console.WriteLine(string.Join(", ", _selectedOptions));
        
        PopulateListBox();
        ValueUpdated?.Invoke(this, EventArgs.Empty);
    }
}