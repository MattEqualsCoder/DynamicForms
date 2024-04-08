using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormNumericUpDown : UserControl
{
    private bool _isInt;
    private bool _isDouble;
    private bool _isFloat;
    private double _increment = 1;
    private double _minValue = int.MinValue;
    private double _maxValue = int.MaxValue;
    private string _previousText = "";
    
    public DynamicFormNumericUpDown(DynamicFormFieldNumericUpDownAttribute upDownAttributes, object value, Type type)
    {
        _increment = upDownAttributes.Increment;
        _minValue = upDownAttributes.MinValue;
        _maxValue = upDownAttributes.MaxValue;
        
        if (type == typeof(int))
        {
            _isInt = true;
        }
        else if (type == typeof(double))
        {
            _isDouble = true;
        }
        else if (type == typeof(float))
        {
            _isFloat = true;
        }
        
        InitializeComponent();
        SetValue(value);
    }

    public object Value { get; private set; } = null!;

    public int IntValue => (int)Value;
    
    public double DoubleValue => (double)Value;
    
    public float FloatValue => (float)Value;
    
    public decimal DecimalValue => (decimal)Value;

    public event EventHandler? ValueChanged;

    public void SetValue(object value)
    {
        if (_isInt)
        {
            Value = Math.Clamp(Convert.ToInt32(value), (int)_minValue, (int)_maxValue);
        }
        else if (_isDouble)
        {
            Value = Math.Clamp(Convert.ToDouble(value), _minValue, _maxValue);
        }
        else if (_isFloat)
        {
            Value = (float)Math.Clamp(Convert.ToDouble(value), _minValue, _maxValue);
        }
        else
        {
            Value = Math.Clamp(Convert.ToDecimal(value), Convert.ToDecimal(_minValue), Convert.ToDecimal(_maxValue));
        }

        MainTextBox.Text = Value.ToString() ?? "0";
        _previousText = MainTextBox.Text;
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (_isInt)
        {
            SetValue(IntValue + _increment);
        }
        else if (_isDouble)
        {
            SetValue(DoubleValue + _increment);
        }
        else if (_isFloat)
        {
            SetValue(FloatValue + _increment);
        }
        else
        {
            SetValue(DecimalValue + Convert.ToDecimal(_increment));
        }
    }

    private void DownButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (_isInt)
        {
            SetValue(IntValue - _increment);
        }
        else if (_isDouble)
        {
            SetValue(DoubleValue - _increment);
        }
        else if (_isFloat)
        {
            SetValue(FloatValue - _increment);
        }
        else
        {
            SetValue(DecimalValue - Convert.ToDecimal(_increment));
        }
    }

    private void MainTextBox_OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (MainTextBox.Text == _previousText)
        {
            return;
        }
        
        try
        {
            SetValue(MainTextBox.Text);
        }
        catch
        {
            SetValue(_previousText);
        }
    }
}