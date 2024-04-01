using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using AvaloniaControls.Controls;
using DynamicForms.Library.Avalonia.Groups;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        var labeledControl = this.Find<LabeledControl>(nameof(MainControl))!;
        labeledControl.Text = formField.Attributes.LabelText;

        if (formField.Type is DynamicFormFieldType.Button)
        {
            labeledControl.Content = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Children = { BodyControl }
            };
        }
        else
        {
            labeledControl.Content = BodyControl;
        }
        
    }
}