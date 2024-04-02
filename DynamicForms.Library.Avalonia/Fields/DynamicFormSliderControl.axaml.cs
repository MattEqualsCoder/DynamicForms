using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormSliderControl : UserControl
{
    private string _suffix;
    private bool _isInt;
    private bool _isDecimal;
    private bool _isFloat;
    private Slider _slider;

    public DynamicFormSliderControl() : this(0, 0, 0, 0, 0, "", typeof(double))
    {
        
    }
    
    public DynamicFormSliderControl(object currentValue, double maximum, double minimum, double incrementAmount, int decimalPlaces, string suffix, Type type)
    {
        _suffix = suffix;

        _isInt = type == typeof(int);
        _isDecimal = type == typeof(decimal);
        _isFloat = type == typeof(float);
        
        InitializeComponent();
        
        _slider = this.Find<Slider>(nameof(ValueSlider))!;
        _slider.Maximum = maximum;
        _slider.Minimum = minimum;
        _slider.TickFrequency = incrementAmount;
        _slider.Value = Convert.ToDouble(currentValue);

        var textBox = this.Find<TextBlock>(nameof(ValueTextBox))!;
        textBox.Text = _slider.Value.ToString(CultureInfo.CurrentCulture) + _suffix;

        _slider.ValueChanged += (sender, args) =>
        {
            if (decimalPlaces == 0)
            {
                var value = Convert.ToInt32(args.NewValue);
                textBox.Text = value.ToString(CultureInfo.CurrentCulture) + _suffix;
                ValueChanged?.Invoke(sender, args);
            }
            else
            {
                var value = Math.Round(args.NewValue, decimalPlaces);
                textBox.Text = value.ToString(CultureInfo.CurrentCulture) + _suffix;
                ValueChanged?.Invoke(sender, args);
            }
        };
    }

    public void SetValue(double value)
    {
        this.Find<Slider>(nameof(ValueSlider))!.Value = value;
        this.Find<TextBlock>(nameof(ValueTextBox))!.Text =
            value.ToString(CultureInfo.CurrentCulture) + _suffix;
    }
    
    public object GetValue()
    {
        if (_isInt)
        {
            return Convert.ToInt32(_slider.Value);
        }
        else if (_isDecimal)
        {
            return Convert.ToDecimal(_slider.Value);
        }
        else if (_isFloat)
        {
            return (float)_slider.Value;
        }

        return _slider.Value;
    }

    public event EventHandler<RangeBaseValueChangedEventArgs>? ValueChanged;
}