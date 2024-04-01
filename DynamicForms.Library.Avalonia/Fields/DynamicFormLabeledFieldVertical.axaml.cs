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
        
        if (string.IsNullOrEmpty(formField.Attributes.LabelText))
        {
            this.Find<TextBlock>(nameof(MainLabel))!.IsVisible = false;
        }
        else
        {
            this.Find<TextBlock>(nameof(MainLabel))!.Text = formField.Attributes.LabelText;
        }
        
        this.Find<StackPanel>(nameof(StackPanel))!.Children.Add(BodyControl);
    }
}