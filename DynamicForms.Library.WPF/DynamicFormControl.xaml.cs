using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using DynamicForms.Library.WPF.Groups;

namespace DynamicForms.Library.WPF;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class DynamicFormControl : UserControl
{
    private bool _loaded;
    
    public DynamicFormControl()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty DataProperty 
        = DependencyProperty.Register( 
            nameof(Data), 
            typeof( object ), 
            typeof( DynamicFormControl ), 
            new PropertyMetadata( false ) 
        );
    
    public object? Data
    {
        get => GetValue(DataProperty);
        set
        {
            SetValue(DataProperty, value);
            LoadDataObject();
        }
    }

    private void DynamicFormControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_loaded)
        {
            return;
        }
        
        LoadDataObject();
        _loaded = true;
    }
    
    private void LoadDataObject()
    {
        if (Data == null)
        {
            return;
        }
        
        var dynamicForm = new DynamicForm(Data);

        var mainGroupControl = CreateFormGroup(dynamicForm.ParentGroup.GroupName, dynamicForm.ParentGroup.Style,
            dynamicForm.ParentGroup.Type,
            dynamicForm.ParentGroup.Objects,
            dynamicForm.ParentGroup.Attributes);

        ParentPanel.Children.Add(mainGroupControl);
    }

    private Control CreateFormGroup(string groupName, DynamicFormGroupStyle style, DynamicFormLayout type, List<DynamicFormObject> groupObjects, DynamicFormGroupAttribute? attribute)
    {
        DynamicFormGroupStyleControl groupStyleControl = style switch
        {
            DynamicFormGroupStyle.Basic => new DynamicFormGroupStyleBasic(),
            DynamicFormGroupStyle.GroupBox => new DynamicFormGroupStyleGroupBox(groupName),
            DynamicFormGroupStyle.Expander => new DynamicFormGroupStyleExpander(groupName, (attribute as DynamicFormGroupExpanderAttribute)?.IsExpanded ?? false),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
            
        DynamicFormGroupLayoutControl groupLayoutControl = type switch
        {
            DynamicFormLayout.Vertical => new DynamicFormGroupLayoutControlVertical(),
            DynamicFormLayout.TwoColumns => new DynamicFormGroupLayoutControlTwoColumn(),
            DynamicFormLayout.SideBySide => new DynamicFormGroupLayoutControlSideBySide(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        var lastGroup = groupObjects.LastOrDefault(x => x is DynamicFormGroup);

        foreach (var formObject in groupObjects)
        {
            if (formObject is DynamicFormField field)
            {
                groupLayoutControl.AddField(field);
            }
            else if (formObject is DynamicFormGroup group)
            {
                var subGroupControl = CreateFormGroup(group.GroupName, group.Style, group.Type, group.Objects, group.Attributes);
                if (formObject != lastGroup)
                {
                    subGroupControl.Margin = new Thickness(0, 0, 0, 5);
                }
                groupLayoutControl.AddControl(subGroupControl);
            }
            else
            {
                throw new InvalidOperationException($"Unknown object type {formObject.GetType().Name}");
            }
        }
        
        groupStyleControl.AddBody(groupLayoutControl);

        return groupStyleControl;
    }
}