using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormLabeledFieldVertical : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldVertical(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        if (string.IsNullOrEmpty(formField.Attributes.LabelText))
        {
            MainLabel.Visibility = Visibility.Collapsed;
        }
        else
        {
            MainLabel.Text = formField.Attributes.LabelText;
        }

        StackPanel.Children.Add(BodyControl);
    }
}