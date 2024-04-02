using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using DynamicForms.Library.Core.Shared;

namespace DynamicForms.Library.WPF.Fields;

public class DynamicFormLabeledField : UserControl
{
    protected FrameworkElement BodyControl { get; private set; }
    
    public DynamicFormLabeledField(DynamicFormField formField)
    {
        switch (formField.Attributes.FieldType)
        {
            case DynamicFormFieldType.TextBox:
                BodyControl = GetTextBox(formField);
                break;
            case DynamicFormFieldType.Text:
                BodyControl = GetTextBlock(formField);
                break;
            case DynamicFormFieldType.CheckBox:
                BodyControl = GetCheckBox(formField);
                break;
            case DynamicFormFieldType.ComboBox:
                BodyControl = GetComboBox(formField);
                break;
            case DynamicFormFieldType.Slider:
                BodyControl = GetSlider(formField);
                break;
            case DynamicFormFieldType.ColorPicker:
                BodyControl = GetColorPicker(formField);
                break;
            case DynamicFormFieldType.FilePicker:
                BodyControl = GetFilePicker(formField);
                break;
            case DynamicFormFieldType.NumericUpDown:
                BodyControl = GetNumericUpDown(formField);
                break;
            case DynamicFormFieldType.EnableDisableReorderList:
                BodyControl = GetEnableDisableReorderList(formField);
                break;
            case DynamicFormFieldType.Button:
                BodyControl = GetButton(formField);
                break;
            default:
                throw new InvalidOperationException("Unknown DynamicFormFieldType");
        }
        
        if (!string.IsNullOrEmpty(formField.Attributes.VisibleWhenProperty))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.VisibleWhenProperty);
            Visibility = (bool?)property?.GetValue(formField.ParentObject) != false ? Visibility.Visible : Visibility.Collapsed;

            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.VisibleWhenProperty)
                    {
                        return;
                    }
                    Visibility = (bool?)property?.GetValue(formField.ParentObject) != false ? Visibility.Visible : Visibility.Collapsed;
                };
            }
        }
        
        if (!string.IsNullOrEmpty(formField.Attributes.EditableWhenProperty))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.EditableWhenProperty);
            BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;
            
            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.EditableWhenProperty)
                    {
                        return;
                    }
                    BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;
                };
            }
        }
    }
    
    private TextBox GetTextBox(DynamicFormField formField)
    {
        var control = new TextBox() { Text = formField.Value as string ?? "" };
        
        control.TextChanged += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.Text);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.Text = formField.GetValue(formField.ParentObject) as string ?? "";
            };
        }

        return control;
    }
    
    private TextBlock GetTextBlock(DynamicFormField formField)
    {
        var control = new TextBlock() { Text = formField.Value as string, TextWrapping = TextWrapping.Wrap };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.Text = formField.GetValue(formField.ParentObject) as string ?? "";
            };
        }

        return control;
    }
    
    private CheckBox GetCheckBox(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldCheckBoxAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for CheckBox control");
        }
        
        var control = new CheckBox() { IsChecked = formField.Value as bool?, Content = attributes.CheckBoxText};
        
        control.Checked += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.IsChecked);
        };
        
        control.Unchecked += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.IsChecked);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.IsChecked = formField.GetValue(formField.ParentObject) as bool?;
            };
        }

        return control;
    }
    
    private ComboBox GetComboBox(DynamicFormField formField)
    {
        if (formField.Value is Enum)
        {
            return GetEnumComboBox(formField);
        }
        else if (formField.Attributes is DynamicFormFieldComboBoxAttribute attributes)
        {
            var optionsProperty = formField.ParentObject.GetType().GetProperties()
                .FirstOrDefault(x => x.Name == attributes.ComboBoxOptionsProperty);

            if (optionsProperty == null)
            {
                throw new InvalidOperationException(
                    "ComboBox specified without a ComboBoxOptionsProperty being designated");
            }

            var options = optionsProperty.GetValue(formField.ParentObject) as ICollection<string>;

            if (options == null)
            {
                throw new InvalidOperationException("ComboBoxOptionsProperty must be a collection of strings");
            }
            
            var control = new ComboBox() { ItemsSource = options, SelectedItem = formField.Value };
        
            control.SelectionChanged += (sender, args) =>
            {
                formField.SetValue(formField.ParentObject, control.SelectedItem);
            };
        
            if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.PropertyName)
                    {
                        return;
                    }

                    control.SelectedItem = formField.GetValue(formField.ParentObject) as string;
                };
            }

            return control;
        }
        else
        {
            throw new InvalidOperationException("Invalid DynamicFormField object for ComboBox");
        }
        
        /*var optionsProperty = formField.ParentObject.GetType().GetProperties()
            .FirstOrDefault(x => x.Name == formField.Attributes.ComboBoxOptionsProperty);

        if (optionsProperty == null)
        {
            throw new InvalidOperationException(
                "ComboBox specified without a ComboBoxOptionsProperty being designated");
        }

        var options = optionsProperty.GetValue(formField.ParentObject) as ICollection<string>;

        if (options == null)
        {
            throw new InvalidOperationException("ComboBoxOptionsProperty must be a collection of strings");
        }
            
        var control = new ComboBox() { ItemsSource = options, SelectedItem = formField.Value };
        
        control.SelectionChanged += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.SelectedItem);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.SelectedItem = formField.GetValue(formField.ParentObject) as string;
            };
        }

        return control;*/
    }
    
    private ComboBox GetEnumComboBox(DynamicFormField formField)
    {
        if (formField.Value == null)
        {
            throw new InvalidOperationException();
        }

        Dictionary<string, Enum> descriptionToEnums = new();
        var selectedItem = "";
        
        foreach (var enumValue in Enum.GetValues(formField.Value.GetType()).Cast<Enum>())
        {
            descriptionToEnums.Add(enumValue.GetDescription(), enumValue);

            if (Equals(enumValue, formField.Value))
            {
                selectedItem = enumValue.GetDescription();
            }
        }
        
        var control = new ComboBox() { ItemsSource = descriptionToEnums.Keys, SelectedItem = string.IsNullOrEmpty(selectedItem) ? descriptionToEnums.Keys.First() : selectedItem };
        
        control.SelectionChanged += (sender, args) =>
        {
            
            formField.SetValue(formField.ParentObject, descriptionToEnums[(string)control.SelectedItem]);
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.SelectedItem = ((Enum)formField.GetValue(formField.ParentObject)!).GetDescription();
            };
        }

        return control;
    }
    
    private Control GetSlider(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldSliderAttribute attributes)
        {
            throw new InvalidOperationException("Invalid DynamicFormField object for Slider");
        }

        var underlyingType = formField.Property?.PropertyType.GetUnderlyingType() ?? typeof(double);
        
        var decimalPlaces = attributes.DecimalPlaces;
        if (underlyingType == typeof(int))
        {
            decimalPlaces = 0;
        }
        
        var incrementAmount = attributes.IncrementAmount;
        if (Math.Abs(incrementAmount + 1) < .001)
        {
            incrementAmount = Math.Pow(0.1, decimalPlaces);
        }
        
        var control = new DynamicFormSliderControl(formField.Value ?? attributes.MinimumValue, attributes.MaximumValue, attributes.MinimumValue, incrementAmount, decimalPlaces, attributes.Suffix, underlyingType);
        
        control.ValueChanged += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.GetValue());
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.SetValue(Convert.ToDouble(formField.GetValue(formField.ParentObject)));
            };
        }

        return control;
    }
    
    private Control GetColorPicker(DynamicFormField formField)
    {
        var bytes = formField.Value as byte[] ?? [0, 0, 0, 0];
        
        var control = new DynamicFormColorPicker(bytes);
        
        control.ValueChanged += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.Value);
        };

        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.SetValue(formField.GetValue(formField.ParentObject) as byte[] ?? [0, 0, 0, 0]);
            };
        }

        return control;
    }

    private Control GetFilePicker(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldFilePickerAttribute filePickerAttribute)
        {
            throw new InvalidOperationException("Invalid value type for ColorPicker");
        }
        
        var value = formField.Value as string ?? "";

        var control = new DynamicFormFilePicker(filePickerAttribute, value);
        
        control.ValueChanged += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.Value);
        };

        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.SetValue(formField.GetValue(formField.ParentObject) as string ?? "");
            };
        }

        return control;
    }
    
    private Control GetNumericUpDown(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldNumericUpDownAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for NumericUpDown control");
        }

        var underlyingType = formField.Property?.PropertyType.GetUnderlyingType() ?? typeof(decimal);
        var control = new DynamicFormNumericUpDown(attributes, formField.Value ?? 0, underlyingType);

        return control;
    }
    
    private Control GetEnableDisableReorderList(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldEnableDisableReorderAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for EnableDisableReorder control");
        }
        
        var selectedOptions = formField.Value as ICollection<string> ?? [];

        var optionsProperty = formField.ParentObject.GetType().GetProperties()
                                  .FirstOrDefault(x => x.Name == attributes.OptionsProperty)
                              ?? throw new InvalidOperationException(
                                  $"Options property {attributes.OptionsProperty} for {formField.Attributes.LabelText} was not found");

        if (optionsProperty.GetValue(formField.ParentObject) is not ICollection<string> options)
        {
            throw new InvalidOperationException("OptionsProperty must be of type ICollection>string>");
        }
        
        var control = new DynamicFormEnableDisableReorderControl(options, selectedOptions, formField.Property?.PropertyType.GetUnderlyingType() ?? typeof(string[]));

        control.ValueUpdated += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.GetValue());
        };

        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.SetValue(formField.GetValue(formField.ParentObject) as ICollection<string> ?? []);
            };
        }

        return control;
    }
    
    private Control GetButton(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldButtonAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for Button control");
        }
        
        var eventName = formField.Value as string;
        
        if (string.IsNullOrEmpty(eventName))
        {
            throw new InvalidOperationException("Invalid button event name");
        }

        var control = new Button() { Content = attributes.ButtonText };

        control.Click += (sender, args) =>
        {
            var parentObject = formField.ParentObject;
            ((Delegate?)parentObject
                    .GetType()
                    .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)!
                    .GetValue(parentObject))?
                .DynamicInvoke(parentObject, EventArgs.Empty);
        };

        return control;
    }
}