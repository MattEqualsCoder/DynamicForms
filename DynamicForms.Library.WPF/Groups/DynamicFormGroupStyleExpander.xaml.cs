using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Groups;

public partial class DynamicFormGroupStyleExpander : DynamicFormGroupStyleControl
{
    public DynamicFormGroupStyleExpander(string groupName, bool isExpanded)
    {
        InitializeComponent();
        Expander.Header = groupName;
        Expander.IsExpanded = isExpanded;
    }

    public override void AddBody(DynamicFormGroupLayoutControl layoutControl)
    {
        MainPanel.Children.Add(layoutControl);
    }
}