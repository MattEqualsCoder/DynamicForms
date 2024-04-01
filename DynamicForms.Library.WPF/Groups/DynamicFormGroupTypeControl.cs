﻿using System.Windows.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.WPF.Groups;

public abstract class DynamicFormGroupTypeControl : UserControl
{
    public abstract void AddField(DynamicFormField field);

    public abstract void AddControl(Control control);
}