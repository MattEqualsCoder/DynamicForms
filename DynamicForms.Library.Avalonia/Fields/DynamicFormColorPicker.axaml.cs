using Avalonia.Controls;
using Avalonia.Media;
using DynamicForms.Library.Core.Shared;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormColorPicker : UserControl
{
    private readonly TextBox _colorTextBox;
    private readonly TextBox _rectangleTextBox;

    public DynamicFormColorPicker() : this([0, 0, 0, 0])
    {
    }
    

    public DynamicFormColorPicker(byte[] bytes)
    {
        InitializeComponent();

        _colorTextBox = this.Find<TextBox>(nameof(ColorTextBox))!;
        _rectangleTextBox = this.Find<TextBox>(nameof(ColorRectangle))!;
        SetValue(bytes);
    }
    
    public byte[] Value { get; private set; } = null!;
    
    public event EventHandler? ValueChanged;

    public void SetValue(byte[] bytes)
    {
        _colorTextBox.Text = StringColorConverter.Convert(bytes);
        _rectangleTextBox.Background = new SolidColorBrush(Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]));
        Value = bytes;
    }
    
    private void ColorTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var bytes = StringColorConverter.Convert(_colorTextBox.Text ?? "#00000000");
        _rectangleTextBox.Background = new SolidColorBrush(Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]));
        Value = bytes;
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

}