namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldSliderAttribute(
    double minimumValue,
    double maximumValue,
    string labelText,
    int decimalPlaces = 1,
    double incrementAmount = -1,
    string suffix = "",
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.Slider;

    public double MinimumValue { get; } = minimumValue;
    public double MaximumValue { get; } = maximumValue;
    public double IncrementAmount { get; } = incrementAmount;
    public int DecimalPlaces { get; } = decimalPlaces;
    public string Suffix { get; } = suffix;

}