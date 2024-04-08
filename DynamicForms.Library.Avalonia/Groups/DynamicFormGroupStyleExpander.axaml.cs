using Avalonia.Controls;
using AvaloniaControls.Controls;

namespace DynamicForms.Library.Avalonia.Groups;

public partial class DynamicFormGroupStyleExpander : DynamicFormGroupStyleControl
{
    private ExpanderControl _expander;

    public DynamicFormGroupStyleExpander() : this("", false)
    {
        
    }
    
    public DynamicFormGroupStyleExpander(string groupName, bool isExpanded)
    {
        InitializeComponent();
        _expander = this.Find<ExpanderControl>(nameof(MainPanel))!;
        _expander.HeaderText = groupName;
        _expander.IsContentVisible = isExpanded;
    }

    public override void AddBody(DynamicFormGroupLayoutControl layoutControl)
    {
        _expander.Content = layoutControl;
    }
}