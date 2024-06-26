﻿using System.Windows.Controls;

namespace DynamicForms.Library.WPF.Groups;

public partial class DynamicFormGroupStyleGroupBox : DynamicFormGroupStyleControl
{
    public DynamicFormGroupStyleGroupBox(string groupName)
    {
        InitializeComponent();
        MainPanel.Header = groupName;
    }

    public override void AddBody(DynamicFormGroupLayoutControl layoutControl)
    {
        MainPanel.Content = layoutControl;
    }
}