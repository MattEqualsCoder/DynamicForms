using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.Avalonia.Groups;

public partial class DynamicFormGroupLayoutVertical : DynamicFormGroupLayoutControl
{
    private StackPanel _mainPanel;
    
    public DynamicFormGroupLayoutVertical()
    {
        InitializeComponent();
        _mainPanel = this.Find<StackPanel>(nameof(MainPanel))!;
    }

    public override void AddField(DynamicFormField field)
    {
        AddControl(new Fields.DynamicFormLabeledFieldVertical(field));
    }

    public override void AddControl(Control control)
    {
        _mainPanel.Children.Add(control);
    }
}