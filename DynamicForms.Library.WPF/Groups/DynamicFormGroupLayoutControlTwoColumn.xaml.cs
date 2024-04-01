using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;
using DynamicForms.Library.WPF.Fields;

namespace DynamicForms.Library.WPF.Groups;

public partial class DynamicFormGroupLayoutControlTwoColumn : DynamicFormGroupLayoutControl
{
    public DynamicFormGroupLayoutControlTwoColumn()
    {
        InitializeComponent();
    }
    
    public override void AddField(DynamicFormField field)
    {
        AddControl(new DynamicFormLabeledFieldVertical(field));
    }

    public override void AddControl(Control control)
    {
        MainPanel.Children.Add(control);
        var count = MainPanel.Children.Count - 1;
        Grid.SetColumn(control, count % 2);
        Grid.SetRow(control, count / 2);

        if (MainPanel.RowDefinitions.Count < count / 2 + 1)
        {
            MainPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }
    }
}