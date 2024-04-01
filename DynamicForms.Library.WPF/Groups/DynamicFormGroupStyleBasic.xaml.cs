using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Groups;

public partial class DynamicFormGroupStyleBasic : DynamicFormGroupStyleControl
{
    public DynamicFormGroupStyleBasic()
    {
        InitializeComponent();
    }

    public override void AddBody(DynamicFormGroupTypeControl typeControl)
    {
        MainPanel.Children.Add(typeControl);
    }
}