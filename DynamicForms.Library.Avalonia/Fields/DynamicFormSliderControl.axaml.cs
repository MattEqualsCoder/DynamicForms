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
    private string? _minimumValueLabel;
    private string? _maximumValueLabel;

    public DynamicFormSliderControl() : this(0, 0, 0, 0, 0, "", typeof(double), null, null, 50)
    {
        
    }
    
    public DynamicFormSliderControl(object currentValue, double maximum, double minimum, double incrementAmount, int decimalPlaces, string suffix, Type type, string? maximumValueLabel, string? minimumValueLabel, int valueDisplayWidth = 50)
    {
        _suffix = suffix;

        _isInt = type == typeof(int);
        _isDecimal = type == typeof(decimal);
        _isFloat = type == typeof(float);
        _minimumValueLabel = minimumValueLabel;
        _maximumValueLabel = maximumValueLabel;
        
        InitializeComponent();
        
        _slider = this.Find<Slider>(nameof(ValueSlider))!;
        _slider.Maximum = maximum;
        _slider.Minimum = minimum;
        _slider.TickFrequency = incrementAmount;
        _slider.Value = Convert.ToDouble(currentValue);
        
        this.Find<TextBlock>(nameof(ValueTextBox))!.Width = valueDisplayWidth;

        UpdateTextBox(_slider.Value);

        _slider.ValueChanged += (sender, args) =>
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
        this.Find<Slider>(nameof(ValueSlider))!.Value = value;
        UpdateTextBox(value);
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

    private void UpdateTextBox(double value)
    {
        if (!string.IsNullOrEmpty(_minimumValueLabel) && value <= _slider.Minimum)
        {
            this.Find<TextBlock>(nameof(ValueTextBox))!.Text = _minimumValueLabel;
        }
        else if (!string.IsNullOrEmpty(_maximumValueLabel) && value >= _slider.Maximum)
        {
            this.Find<TextBlock>(nameof(ValueTextBox))!.Text = _maximumValueLabel;
        }
        else
        {
            this.Find<TextBlock>(nameof(ValueTextBox))!.Text =
                value.ToString(CultureInfo.CurrentCulture) + _suffix;
        }
    }

    public event EventHandler<RangeBaseValueChangedEventArgs>? ValueChanged;
}