using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Groups;

public abstract class DynamicFormGroupStyleControl : UserControl
{
    public abstract void AddBody(DynamicFormGroupTypeControl typeControl);
}