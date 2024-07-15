namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a text box for a number with up/down increment/decrement buttons
/// </summary>
/// <param name="increment">Value to increment/decrement the value by</param>
/// <param name="minValue">The minimum value allowed by the control</param>
/// <param name="maxValue">The maximum value allowed by the control</param>
/// <param name="label">The form label text</param>
/// <param name="labelIsProperty">If the label is a property instead of text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenTrue">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenTrue">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
/// <param name="platforms">The platform(s) the object should be displayed on</param>
[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldNumericUpDownAttribute(
    double increment = 1,
    double minValue = int.MinValue,
    double maxValue = int.MaxValue,
    string label = "",
    bool labelIsProperty = false,
    string? toolTipText = null,
    string? visibleWhenTrue = null,
    string? editableWhenTrue = null,
    string groupName = "",
    int order = int.MaxValue,
    DynamicFormPlatform platforms = DynamicFormPlatform.All)
    : DynamicFormFieldAttribute(label, labelIsProperty, toolTipText, visibleWhenTrue, editableWhenTrue, groupName, order, platforms)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.NumericUpDown;
    public override ICollection<Type> AllowedTypes => [typeof(int), typeof(double), typeof(float), typeof(decimal)];

    public double Increment { get; } = increment;

    public double MinValue { get; } = minValue;

    public double MaxValue { get; } = maxValue;
}