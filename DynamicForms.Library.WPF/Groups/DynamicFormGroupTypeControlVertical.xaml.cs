using System.Windows.Controls;
using DynamicForms.Library.Core;
using DynamicForms.Library.WPF.Fields;

namespace DynamicForms.Library.WPF.Groups;

public partial class DynamicFormGroupTypeControlVertical : DynamicFormGroupTypeControl
{
    public DynamicFormGroupTypeControlVertical()
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
    }
}