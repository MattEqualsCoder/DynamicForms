using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        MainGrid.ToolTip = formField.Attributes.ToolTipText;
        
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
        MainLabel.Content = text;
    }
    
}