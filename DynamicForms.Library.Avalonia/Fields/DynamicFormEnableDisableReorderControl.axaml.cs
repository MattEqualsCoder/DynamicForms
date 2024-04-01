using Avalonia.Controls;
using DynamicData;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormEnableDisableReorderControl : UserControl
{
    private readonly ListBox _listBox;
    private readonly bool _isArray;
    private readonly ICollection<string> _options;
    private ICollection<string> _selectedOptions;
    
    public DynamicFormEnableDisableReorderControl(ICollection<string> options, ICollection<string> selectedOptions)
    {
        InitializeComponent();

        if (selectedOptions is string[])
        {
            _isArray = true;
        }
        
        _listBox = this.Find<ListBox>(nameof(MainListBox))!;
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
        _listBox.Items.Clear();

        foreach (var item in _selectedOptions)
        {
            var itemControl = new DynamicFormEnableDisableReorderControlItem(true, item,
                item != _selectedOptions.First(), item != _selectedOptions.Last());
            _listBox.Items.Add(new ListBoxItem() { Content = itemControl});
            itemControl.OnCheckChanged += ItemControlOnOnCheckChanged;
        }
        
        foreach (var item in _options.Where(x => !_selectedOptions.Contains(x)))
        {
            var itemControl = new DynamicFormEnableDisableReorderControlItem(false, item, false, false);
            _listBox.Items.Add(new ListBoxItem() { Content = itemControl});
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
            var index = _selectedOptions.IndexOf(item.OptionName) - 1;
            var newList = _selectedOptions.Where(x => x != item.OptionName).ToList();
            newList.Insert(index, item.OptionName);
            _selectedOptions = newList;
        }
        else if (item.MoveDown)
        {
            var index = _selectedOptions.IndexOf(item.OptionName) + 1;
            var newList = _selectedOptions.Where(x => x != item.OptionName).ToList();
            newList.Insert(index, item.OptionName);
            _selectedOptions = newList;
        }
        
        PopulateListBox();
        ValueUpdated?.Invoke(this, EventArgs.Empty);
    }
}
