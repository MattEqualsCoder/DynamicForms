using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using AvaloniaControls.Controls;
using DynamicForms.Library.Avalonia.Groups;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormLabeledFieldSideBySide : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldSideBySide() : this(new DynamicFormField("", "", null, new DynamicFormFieldTextAttribute(), ""))
    {
        
    }
    
    public DynamicFormLabeledFieldSideBySide(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        var labeledControl = this.Find<LabeledControl>(nameof(MainControl))!;
        labeledControl.Text = formField.Attributes.Label;
        
        ToolTip.SetTip(labeledControl, formField.Attributes.ToolTipText);

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
        var labeledControl = this.Find<LabeledControl>(nameof(MainControl))!;
        labeledControl.Text = text;
    }
}