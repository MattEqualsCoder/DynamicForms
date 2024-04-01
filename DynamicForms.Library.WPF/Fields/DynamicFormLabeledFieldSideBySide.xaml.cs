using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        MainLabel.Content = formField.Attributes.LabelText;
        
        if (formField.Type is DynamicFormFieldType.Button)
        {
            MainContent.Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Children = { BodyControl }
            };
        }
        else
        {
            MainContent.Content = BodyControl;
        }
    }
    
}