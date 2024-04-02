namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a text box for a number with up/down increment/decrement buttons
/// </summary>
/// <param name="increment">Value to increment/decrement the value by</param>
/// <param name="minValue">The minimum value allowed by the control</param>
/// <param name="maxValue">The maximum value allowed by the control</param>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenProperty">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenProperty">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldNumericUpDownAttribute(
    double increment = 1,
    double minValue = int.MinValue,
    double maxValue = int.MaxValue,
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.NumericUpDown;
    public override ICollection<Type>? AllowedTypes => [typeof(int), typeof(double), typeof(float), typeof(decimal)];

    public double Increment { get; } = increment;

    public double MinValue { get; } = minValue;

    public double MaxValue { get; } = maxValue;
}