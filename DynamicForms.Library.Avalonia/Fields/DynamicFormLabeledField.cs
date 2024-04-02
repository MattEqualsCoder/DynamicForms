using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaControls;
using AvaloniaControls.Controls;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using DynamicForms.Library.Core.Shared;

namespace DynamicForms.Library.Avalonia.Fields;

public class DynamicFormLabeledField : UserControl
{
    protected Control BodyControl;

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

        if (!string.IsNullOrEmpty(formField.Attributes.VisibleWhenTrue))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.VisibleWhenTrue);
            IsVisible = (bool?)property?.GetValue(formField.ParentObject) != false;

            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.VisibleWhenTrue)
                    {
                        return;
                    }
                    IsVisible = (bool?)property?.GetValue(formField.ParentObject) != false;
                };
            }
        }
        
        if (!string.IsNullOrEmpty(formField.Attributes.EditableWhenTrue))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.EditableWhenTrue);
            BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;
            
            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.EditableWhenTrue)
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
        var control = new TextBox() { Text = formField.Value?.ToString() ?? "" };

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
            throw new InvalidOperationException("Invalid DynamicFormField object for CheckBox field");
        }
        
        var control = new CheckBox { IsChecked = formField.Value as bool?, Content = attributes.CheckBoxText };
        
        control.IsCheckedChanged += (sender, args) =>
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
    
    private Control GetComboBox(DynamicFormField formField)
    {
        if (formField.Value is Enum)
        {
            var control = new EnumComboBox() { EnumType = formField.Value?.GetType(), Value = formField.Value };
        
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

                    control.Value = formField.GetValue(formField.ParentObject);
                };
            }
        
            return control;
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
    }

    private Control GetSlider(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldSliderAttribute attributes)
        {
            throw new InvalidOperationException("Invalid DynamicFormField object for Slider");
        }

        var decimalPlaces = attributes.DecimalPlaces;
        var underlyingType = formField.Property?.PropertyType.GetUnderlyingType() ?? typeof(double);
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
        if (formField.Attributes is not DynamicFormFieldFilePickerAttribute attributes)
        {
            throw new InvalidOperationException("Invalid value type for ColorPicker");
        }

        var control = new FileControl()
        {
            FileInputType = attributes.FilePickerType switch
            {
                FilePickerType.OpenFile => FileInputControlType.OpenFile,
                FilePickerType.SaveFile => FileInputControlType.SaveFile,
                _ => FileInputControlType.Folder
            },
            Filter = attributes.Filter,
            FileValidationHash = attributes.CheckSum,
            FileValidationHashError = attributes.CheckSumError
        };
        
        control.OnUpdated += (sender, args) =>
        {
            formField.SetValue(formField.ParentObject, control.FilePath);
        };

        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.FilePath = formField.GetValue(formField.ParentObject) as string ?? "";
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
        
        var control = new NumericUpDown()
        {
            Value = Convert.ToDecimal(formField.Value),
            Increment = Convert.ToDecimal(attributes.Increment),
            Minimum = Convert.ToDecimal(attributes.MinValue),
            Maximum = Convert.ToDecimal(attributes.MaxValue)
        };

        control.ValueChanged += (sender, args) =>
        {
            if (underlyingType == typeof(int))
            {
                formField.SetValue(formField.ParentObject, Convert.ToInt32(control.Value));
            }
            else if (underlyingType == typeof(double))
            {
                formField.SetValue(formField.ParentObject, Convert.ToDouble(control.Value));
            }
            else if (underlyingType == typeof(float))
            {
                formField.SetValue(formField.ParentObject, (float)Convert.ToDouble(control.Value));
            }
            else
            {
                formField.SetValue(formField.ParentObject, control.Value);
            }
        };
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                control.Value = Convert.ToDecimal(formField.GetValue(formField.ParentObject));
            };
        }

        return control;
    }
    
    private Control GetEnableDisableReorderList(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldEnableDisableReorderAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for EnableDisableReorder control");
        }
        
        var underlyingType = formField.Property?.PropertyType.GetUnderlyingType() ?? typeof(string []);

        var selectedOptions = formField.Value as ICollection<string> ?? [];
        
        var optionsProperty = formField.ParentObject.GetType().GetProperties()
                                  .FirstOrDefault(x => x.Name == attributes.OptionsProperty)
                              ?? throw new InvalidOperationException(
                                  $"Options property {attributes.OptionsProperty} for {formField.Attributes.LabelText} was not found");

        if (optionsProperty.GetValue(formField.ParentObject) is not ICollection<string> options)
        {
            throw new InvalidOperationException("OptionsProperty must be of type ICollection>string>");
        }
        
        var control = new DynamicFormEnableDisableReorderControl(options, selectedOptions, underlyingType);

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

        if (!string.IsNullOrEmpty(eventName))
        {
            var control = new Button() { Content = attributes.ButtonText };

            control.Click += (sender, args) =>
            {
                var parentObject = formField.ParentObject;
                ((Delegate?)parentObject
                        .GetType()
                        .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)!
                        .GetValue(parentObject))
                    ?.DynamicInvoke(parentObject, EventArgs.Empty);
            };

            return control;
        }
        else if (formField.Value is ICommand command)
        {
            var control = new Button() { Content = attributes.ButtonText };

            command.CanExecuteChanged += (sender, args) =>
            {
                control.IsEnabled = command.CanExecute(formField.ParentObject);
            };
            
            if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += (sender, args) =>
                {
                    control.IsEnabled = command.CanExecute(formField.ParentObject);
                };
            }
            
            control.IsEnabled = command.CanExecute(formField.ParentObject);

            control.Click += (sender, args) =>
            {
                if (!command.CanExecute(formField.ParentObject))
                {
                    return;
                }

                command.Execute(formField.ParentObject);
            };

            return control;
        }
        else
        {
            throw new InvalidOperationException("Invalid button type");
        }

    }
}