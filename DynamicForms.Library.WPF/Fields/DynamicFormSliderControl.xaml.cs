using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormSliderControl : UserControl
{
    private readonly string _suffix;
    
    public DynamicFormSliderControl(double currentValue, double maximum, double minimum, double incrementAmount, int decimalPlaces, string suffix)
    {
        _suffix = suffix;
        
        InitializeComponent();

        var slider = ValueSlider;
        slider.Maximum = maximum;
        slider.Minimum = minimum;
        slider.TickFrequency = incrementAmount;
        slider.Value = currentValue;

        var textBox = ValueTextBox;
        textBox.Text = currentValue.ToString(CultureInfo.CurrentCulture) + _suffix;

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

    public event EventHandler<RoutedPropertyChangedEventArgs<double>>? ValueChanged;
}