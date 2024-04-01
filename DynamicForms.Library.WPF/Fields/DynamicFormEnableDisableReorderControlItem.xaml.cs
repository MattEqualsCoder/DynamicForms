using System.Windows;
using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormEnableDisableReorderControlItem : UserControl
{
    public DynamicFormEnableDisableReorderControlItem(bool isOptionEnabled, string optionName, bool canMoveUp, bool canMoveDown)
    {
        IsOptionEnabled = isOptionEnabled;
        OptionName = optionName;
        InitializeComponent();
        MainCheckBox.Content = optionName;
        MainCheckBox.IsChecked = isOptionEnabled;
        UpButton.IsEnabled = isOptionEnabled && canMoveUp;
        DownButton.IsEnabled = isOptionEnabled && canMoveDown;
    }
    
    public event EventHandler? OnCheckChanged;
    
    public bool IsOptionEnabled { get; set; }
    
    public string OptionName { get; set; }
    
    public bool IsCheckChanged { get; set; }
    
    public bool MoveUp { get; set;}
    
    public bool MoveDown { get; set; }

    private void UpButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MoveUp = true;
        OnCheckChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DownButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MoveDown = true;
        OnCheckChanged?.Invoke(this, EventArgs.Empty);
    }

    private void MainCheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
        {
            return;
        }

        if (IsOptionEnabled == (checkBox.IsChecked == true))
        {
            return;
        }

        IsCheckChanged = true;
        IsOptionEnabled = checkBox.IsChecked == true;
        OnCheckChanged?.Invoke(this, EventArgs.Empty);
    }
}