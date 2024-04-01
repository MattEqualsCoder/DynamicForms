using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormEnableDisableReorderControlItem : UserControl
{
    public DynamicFormEnableDisableReorderControlItem(bool isOptionEnabled, string optionName, bool canMoveUp, bool canMoveDown)
    {
        IsOptionEnabled = isOptionEnabled;
        OptionName = optionName;
        InitializeComponent();
        this.Find<CheckBox>(nameof(MainCheckBox))!.Content = optionName;
        this.Find<CheckBox>(nameof(MainCheckBox))!.IsChecked = isOptionEnabled;
        this.Find<Button>(nameof(UpButton))!.IsEnabled = isOptionEnabled && canMoveUp;
        this.Find<Button>(nameof(DownButton))!.IsEnabled = isOptionEnabled && canMoveDown;
    }

    public event EventHandler? OnCheckChanged;
    
    public bool IsOptionEnabled { get; set; }
    
    public string OptionName { get; set; }
    
    public bool IsCheckChanged { get; set; }
    
    public bool MoveUp { get; set;}
    
    public bool MoveDown { get; set; }

    private void MainCheckBox_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
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
}