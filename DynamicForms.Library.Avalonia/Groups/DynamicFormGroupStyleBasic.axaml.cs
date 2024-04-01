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

    public override void AddBody(DynamicFormGroupTypeControl typeControl)
    {
        this.Find<DockPanel>(nameof(MainPanel))!.Children.Add(typeControl);
    }
}