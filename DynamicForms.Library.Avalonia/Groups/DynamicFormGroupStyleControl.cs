using Avalonia.Controls;

namespace DynamicForms.Library.Avalonia.Groups;

public abstract class DynamicFormGroupStyleControl : UserControl
{
    public abstract void AddBody(DynamicFormGroupLayoutControl layoutControl);
}