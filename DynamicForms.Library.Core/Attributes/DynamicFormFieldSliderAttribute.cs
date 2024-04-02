namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a slider for numbers with text block displaying the number
/// </summary>
/// <param name="minimumValue">The minimum value allowed by the control</param>
/// <param name="maximumValue">The maximum value allowed by the control</param>
/// <param name="decimalPlaces">Number to round the decimal value</param>
/// <param name="incrementAmount">Value to increment/decrement the value by</param>
/// <param name="suffix">The suffix to display after the number in the text block</param>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenProperty">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenProperty">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldSliderAttribute(
    double minimumValue,
    double maximumValue,
    int decimalPlaces = 1,
    double incrementAmount = -1,
    string suffix = "",
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.Slider;
    public override ICollection<Type>? AllowedTypes => [typeof(int), typeof(double), typeof(float), typeof(decimal)];

    public double MinimumValue { get; } = minimumValue;
    public double MaximumValue { get; } = maximumValue;
    public double IncrementAmount { get; } = incrementAmount;
    public int DecimalPlaces { get; } = decimalPlaces;
    public string Suffix { get; } = suffix;

}