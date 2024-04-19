using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormLabeledFieldVertical : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldVertical(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();

        StackPanel.ToolTip = formField.Attributes.ToolTipText;

        StackPanel.Children.Add(BodyControl);

        if (formField.Attributes.LabelIsProperty)
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.Label);
            var text = property?.GetValue(formField.ParentObject) as string ?? "";
            SetLabelText(text);
        }
        else
        {
            SetLabelText(formField.Attributes.Label);
        }
    }

    public override void SetLabelText(string text)
    {
        MainLabel.Visibility = !string.IsNullOrEmpty(text) ? Visibility.Visible : Visibility.Collapsed;
        textBox.Text = text;
    }
}