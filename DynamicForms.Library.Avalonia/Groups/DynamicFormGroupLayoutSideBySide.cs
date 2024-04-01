using DynamicForms.Library.Core;

namespace DynamicForms.Library.Avalonia.Groups;

public class DynamicFormGroupLayoutSideBySide : DynamicFormGroupLayoutVertical
{
    public override void AddField(DynamicFormField field)
    {
        AddControl(new Fields.DynamicFormLabeledFieldSideBySide(field));
    }
}