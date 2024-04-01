using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Groups;

public partial class DynamicFormGroupStyleExpander : DynamicFormGroupStyleControl
{
    public DynamicFormGroupStyleExpander(string groupName)
    {
        InitializeComponent();
        Expander.Header = groupName;
    }

    public override void AddBody(DynamicFormGroupLayoutControl layoutControl)
    {
        MainPanel.Children.Add(layoutControl);
    }
}