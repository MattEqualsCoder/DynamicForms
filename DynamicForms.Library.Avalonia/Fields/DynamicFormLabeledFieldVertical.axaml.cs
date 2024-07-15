using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Library.Avalonia.Fields;

public partial class DynamicFormLabeledFieldVertical : DynamicFormLabeledField
{
    public DynamicFormLabeledFieldVertical() : this(new DynamicFormField("", "", null,
        new DynamicFormFieldTextAttribute(), ""))
    {
        
    }
    
    public DynamicFormLabeledFieldVertical(DynamicFormField formField) : base(formField)
    {
        InitializeComponent();
        
        var mainPanel = this.Find<StackPanel>(nameof(StackPanel))!;
        mainPanel.Children.Add(BodyControl);
        ToolTip.SetTip(mainPanel, formField.Attributes.ToolTipText);
        
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
        var textBox = this.Find<TextBlock>(nameof(MainLabel))!;
        textBox.IsVisible = !string.IsNullOrEmpty(text);
        textBox.Text = text;
    }
}