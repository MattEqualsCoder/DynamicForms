using Avalonia.Controls;
using AvaloniaControls.Controls;

namespace DynamicForms.Library.Avalonia.Groups;

public partial class DynamicFormGroupStyleExpander : DynamicFormGroupStyleControl
{
    private ExpanderControl _expander;

    public DynamicFormGroupStyleExpander() : this("")
    {
        
    }
    
    public DynamicFormGroupStyleExpander(string groupName)
    {
        InitializeComponent();
        _expander = this.Find<ExpanderControl>(nameof(MainPanel))!;
        _expander.HeaderText = groupName;
    }

    public override void AddBody(DynamicFormGroupTypeControl typeControl)
    {
        _expander.Content = typeControl;
    }
}