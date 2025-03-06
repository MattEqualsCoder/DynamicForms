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
    private string? _minimumValueLabel;
    private string? _maximumValueLabel;
    
    public DynamicFormSliderControl(object currentValue, double maximum, double minimum, double incrementAmount, int decimalPlaces, string suffix, Type type, string? maximumValueLabel, string? minimumValueLabel, int valueDisplayWidth = 50)
    {
        _suffix = suffix;

        _isInt = type == typeof(int);
        _isDecimal = type == typeof(decimal);
        _isFloat = type == typeof(float);
        _minimumValueLabel = minimumValueLabel;
        _maximumValueLabel = maximumValueLabel;
        
        InitializeComponent();

        var slider = ValueSlider;
        slider.Maximum = maximum;
        slider.Minimum = minimum;
        slider.TickFrequency = incrementAmount;
        slider.Value = Convert.ToDouble(currentValue);

        ValueTextBox.Width = valueDisplayWidth;
        UpdateTextBox(Math.Round(slider.Value, decimalPlaces));

        slider.ValueChanged += (sender, args) =>
        {
            if (decimalPlaces == 0)
            {
                var value = Convert.ToInt32(args.NewValue);
                UpdateTextBox(value);
                ValueChanged?.Invoke(sender, args);
            }
            else
            {
                var value = Math.Round(args.NewValue, decimalPlaces);
                UpdateTextBox(value);
                ValueChanged?.Invoke(sender, args);
            }
        };
    }
    
    public void SetValue(double value)
    {
        ValueSlider.Value = value;
        UpdateTextBox(value);
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

    private void UpdateTextBox(double value)
    {
        if (!string.IsNullOrEmpty(_minimumValueLabel) && value <= ValueSlider.Minimum)
        {
            ValueTextBox.Text = _minimumValueLabel;
        }
        else if (!string.IsNullOrEmpty(_maximumValueLabel) && value >= ValueSlider.Maximum)
        {
            ValueTextBox.Text = _maximumValueLabel;
        }
        else
        {
            ValueTextBox.Text = value.ToString(CultureInfo.CurrentCulture) + _suffix;
        }
    }

    public event EventHandler<RoutedPropertyChangedEventArgs<double>>? ValueChanged;
}