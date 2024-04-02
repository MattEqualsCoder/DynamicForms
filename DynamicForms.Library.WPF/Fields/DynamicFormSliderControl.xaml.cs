using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormSliderControl : UserControl
{
    private readonly string _suffix;
    private readonly bool _isInt;
    private readonly bool _isDecimal;
    private readonly bool _isFloat;
    
    public DynamicFormSliderControl(object currentValue, double maximum, double minimum, double incrementAmount, int decimalPlaces, string suffix, Type type)
    {
        _suffix = suffix;

        _isInt = type == typeof(int);
        _isDecimal = type == typeof(decimal);
        _isFloat = type == typeof(float);
        
        InitializeComponent();

        var slider = ValueSlider;
        slider.Maximum = maximum;
        slider.Minimum = minimum;
        slider.TickFrequency = incrementAmount;
        slider.Value = Convert.ToDouble(currentValue);

        var textBox = ValueTextBox;
        textBox.Text = slider.Value.ToString(CultureInfo.CurrentCulture) + _suffix;

        slider.ValueChanged += (sender, args) =>
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
        ValueSlider.Value = value;
        ValueTextBox.Text =
            value.ToString(CultureInfo.CurrentCulture) + _suffix;
    }
    
    public object GetValue()
    {
        if (_isInt)
        {
            return Convert.ToInt32(ValueSlider.Value);
        }
        else if (_isDecimal)
        {
            return Convert.ToDecimal(ValueSlider.Value);
        }
        else if (_isFloat)
        {
            return (float)ValueSlider.Value;
        }

        return ValueSlider.Value;
    }

    public event EventHandler<RoutedPropertyChangedEventArgs<double>>? ValueChanged;
}