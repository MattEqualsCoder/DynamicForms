using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DynamicForms.Library.Avalonia.Groups;

public partial class DynamicFormGroupStyleBasic : DynamicFormGroupStyleControl
{
    public DynamicFormGroupStyleBasic()
    {
        InitializeComponent();
    }

    public override void AddBody(DynamicFormGroupLayoutControl layoutControl)
    {
        this.Find<DockPanel>(nameof(MainPanel))!.Children.Add(layoutControl);
    }
}