using Avalonia;
using Avalonia.Controls;
using DynamicForms.Library.Core;

namespace DynamicForms.Library.Avalonia.Groups;

public partial class DynamicFormGroupLayoutTwoColumn : DynamicFormGroupLayoutControl
{
    private Grid _mainGrid;
    
    public DynamicFormGroupLayoutTwoColumn()
    {
        InitializeComponent();
        _mainGrid = this.Find<Grid>(nameof(MainGrid))!;
    }

    public override void AddField(DynamicFormField field)
    {
        AddControl(new Fields.DynamicFormLabeledFieldVertical(field));
    }

    public override void AddControl(Control control)
    {
        if (_mainGrid.Children.Count % 2 == 0)
        {
            control.Margin = new Thickness(0, 0, 2.5, 0);
        }
        else
        {
            control.Margin = new Thickness(2.5, 0, 0, 0);
        }
        
        Grid.SetRow(control, _mainGrid.Children.Count / 2);
        Grid.SetColumn(control, _mainGrid.Children.Count % 2);
        
        _mainGrid.Children.Add(control);
        
        var rows = new List<string>();
        for (var i = 0; i <= _mainGrid.Children.Count / 2; i++)
        {
            rows.Add("Auto");
        }

        _mainGrid.RowDefinitions = new RowDefinitions(string.Join(", ", rows));
    }
}