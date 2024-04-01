using DynamicForms.Library.Core;
using DynamicForms.Library.WPF.Fields;

namespace DynamicForms.Library.WPF.Groups;

public class DynamicFormGroupLayoutControlSideBySide : DynamicFormGroupLayoutControlVertical
{
    public override void AddField(DynamicFormField field)
    {
        AddControl(new DynamicFormLabeledFieldSideBySide(field));
    }
}
