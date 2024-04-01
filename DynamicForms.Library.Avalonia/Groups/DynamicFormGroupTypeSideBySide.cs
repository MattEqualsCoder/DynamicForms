using DynamicForms.Library.Core;

namespace DynamicForms.Library.Avalonia.Groups;

public class DynamicFormGroupTypeSideBySide : DynamicFormGroupTypeVertical
{
    public override void AddField(DynamicFormField field)
    {
        AddControl(new Fields.DynamicFormLabeledFieldSideBySide(field));
    }
}