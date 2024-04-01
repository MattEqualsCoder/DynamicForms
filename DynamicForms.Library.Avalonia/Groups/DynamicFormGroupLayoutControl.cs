using Avalonia.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.Avalonia.Groups;

public abstract class DynamicFormGroupLayoutControl : UserControl
{
    public abstract void AddField(DynamicFormField field);

    public abstract void AddControl(Control control);
}