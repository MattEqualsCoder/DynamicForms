namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldNumericUpDownAttribute(
    double increment = 1,
    double minValue = int.MinValue,
    double maxValue = int.MaxValue,
    string labelText = "",
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.NumericUpDown;

    public double Increment { get; } = increment;

    public double MinValue { get; } = minValue;

    public double MaxValue { get; } = maxValue;
}