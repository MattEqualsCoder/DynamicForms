using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DynamicForms.Library.Core;
using DynamicForms.Library.Core.Attributes;
using DynamicForms.Library.Core.Shared;

namespace DynamicForms.Library.WPF.Fields;

public abstract class DynamicFormLabeledField : UserControl
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

        if (formField.Attributes.LabelIsProperty)
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.Label);

            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.Label)
                    {
                        return;
                    }
                    
                    if (CheckAccess())
                    {
                        SetLabelText(property!.GetValue(formField.ParentObject) as string ?? "");
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            SetLabelText(property!.GetValue(formField.ParentObject) as string ?? "");
                        });
                    }
                };

            }
        }
        
        if (!string.IsNullOrEmpty(formField.Attributes.VisibleWhenTrue))
        {
            var property = formField.ParentObject.GetType().GetProperty(formField.Attributes.VisibleWhenTrue);
            Visibility = (bool?)property?.GetValue(formField.ParentObject) != false ? Visibility.Visible : Visibility.Collapsed;

            if (formField.ParentObject is INotifyPropertyChanged notifyParent)
            {
                notifyParent.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName != formField.Attributes.VisibleWhenTrue)
                    {
                        return;
                    }

                    if (CheckAccess())
                    {
                        Visibility = (bool?)property?.GetValue(formField.ParentObject) != false ? Visibility.Visible : Visibility.Collapsed;    
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Visibility = (bool?)property?.GetValue(formField.ParentObject) != false
                                ? Visibility.Visible
                                : Visibility.Collapsed;
                        });
                    }
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

                    if (CheckAccess())
                    {
                        BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;    
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            BodyControl.IsEnabled = (bool?)property?.GetValue(formField.ParentObject) != false;
                        });
                    }
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

                if (Dispatcher.CheckAccess())
                {
                    control.Text = formField.GetValue(formField.ParentObject) as string ?? "";    
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.Text = formField.GetValue(formField.ParentObject) as string ?? "";
                    });
                }
            };
        }

        return control;
    }
    
    public abstract void SetLabelText(string text);
    
    private TextBlock GetTextBlock(DynamicFormField formField)
    {
        if (formField.Attributes is not DynamicFormFieldTextAttribute attributes)
        {
            throw new InvalidOperationException("Invalid attribute type for TextBlock control");
        }
        
        var control = new TextBlock() { Text = formField.Value as string, TextWrapping = TextWrapping.Wrap };
        
        if (attributes.Alignment != DynamicFormAlignment.Default)
        {
            control.HorizontalAlignment = attributes.Alignment switch
            {
                DynamicFormAlignment.Right => HorizontalAlignment.Right,
                DynamicFormAlignment.Left => HorizontalAlignment.Left,
                DynamicFormAlignment.Center => HorizontalAlignment.Center,
                DynamicFormAlignment.Stretch => HorizontalAlignment.Stretch,
                _ => control.HorizontalAlignment
            };
        }
        
        if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != formField.PropertyName)
                {
                    return;
                }

                if (CheckAccess())
                {
                    control.Text = formField.GetValue(formField.ParentObject) as string ?? "";    
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.Text = formField.GetValue(formField.ParentObject) as string ?? "";
                    });
                }
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
        
        if (attributes.Alignment != DynamicFormAlignment.Default)
        {
            control.HorizontalAlignment = attributes.Alignment switch
            {
                DynamicFormAlignment.Right => HorizontalAlignment.Right,
                DynamicFormAlignment.Left => HorizontalAlignment.Left,
                DynamicFormAlignment.Center => HorizontalAlignment.Center,
                DynamicFormAlignment.Stretch => HorizontalAlignment.Stretch,
                _ => control.HorizontalAlignment
            };
        }
        
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

                if (CheckAccess())
                {
                    control.IsChecked = formField.GetValue(formField.ParentObject) as bool?;    
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.IsChecked = formField.GetValue(formField.ParentObject) as bool?;
                    });
                }
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

            var value = optionsProperty.GetValue(formField.ParentObject);

            if (value is ICollection<string> options)
            {
                ComboBox control = new ComboBox() { ItemsSource = options, SelectedItem = formField.Value };
                
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

                        if (CheckAccess())
                        {
                            control.SelectedItem = formField.GetValue(formField.ParentObject) as string;
                        }
                        else
                        {
                            Dispatcher.Invoke(() =>
                            {
                                control.SelectedItem = formField.GetValue(formField.ParentObject) as string;
                            });
                        }
                    };
                }

                return control;
                
            }
            else if (value is Dictionary<string, string> map)
            {
                var control = new ComboBox() { ItemsSource = map, SelectedValuePath = "Key", DisplayMemberPath = "Value" };

                control.SelectedItem = map.FirstOrDefault(x => x.Key == formField.Value as string);;
                
                control.SelectionChanged += (sender, args) =>
                {
                    formField.SetValue(formField.ParentObject, (control.SelectedItem as KeyValuePair<string, string>?)?.Key);
                };
        
                if (formField.ParentObject is INotifyPropertyChanged notifyPropertyChanged)
                {
                    notifyPropertyChanged.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName != formField.PropertyName)
                        {
                            return;
                        }

                        if (CheckAccess())
                        {
                            control.SelectedItem = map.First(x =>
                                x.Key == formField.GetValue(formField.ParentObject) as string);
                        }
                        else
                        {
                            Dispatcher.Invoke(() =>
                            {
                                control.SelectedItem = map.First(x =>
                                    x.Key == formField.GetValue(formField.ParentObject) as string);
                            });
                        }
                    };
                }

                return control;
            }
            else
            {
                throw new InvalidOperationException("Invalid type for ComboBox");
            }
            
            
        }
        else
        {
            throw new InvalidOperationException("Invalid DynamicFormField object for ComboBox");
        }
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

                if (CheckAccess())
                {
                    control.SelectedItem = ((Enum)formField.GetValue(formField.ParentObject)!).GetDescription();;
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.SelectedItem = ((Enum)formField.GetValue(formField.ParentObject)!).GetDescription();
                    });
                }
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

                if (CheckAccess())
                {
                    control.SetValue(Convert.ToDouble(formField.GetValue(formField.ParentObject)));
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.SetValue(Convert.ToDouble(formField.GetValue(formField.ParentObject)));
                    });
                }
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

                if (CheckAccess())
                {
                    control.SetValue(formField.GetValue(formField.ParentObject) as byte[] ?? [0, 0, 0, 0]);
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.SetValue(formField.GetValue(formField.ParentObject) as byte[] ?? [0, 0, 0, 0]);
                    });
                }
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

                if (CheckAccess())
                {
                    control.SetValue(formField.GetValue(formField.ParentObject) as string ?? "");
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.SetValue(formField.GetValue(formField.ParentObject) as string ?? "");
                    });
                }
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

                if (CheckAccess())
                {
                    control.SetValue(formField.GetValue(formField.ParentObject) ?? 0);
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        control.SetValue(formField.GetValue(formField.ParentObject) ?? 0);
                    });
                }
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
        
        var selectedOptions = formField.Value as ICollection<string> ?? [];

        var optionsProperty = formField.ParentObject.GetType().GetProperties()
                                  .FirstOrDefault(x => x.Name == attributes.OptionsProperty)
                              ?? throw new InvalidOperationException(
                                  $"Options property {attributes.OptionsProperty} for {formField.Attributes.Label} was not found");

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
                if (CheckAccess())
                {
                    if (args.PropertyName == formField.PropertyName)
                    {
                        control.SetValue(formField.GetValue(formField.ParentObject) as ICollection<string> ?? []);    
                    }
                    else
                    {
                        control.SetOptions(optionsProperty.GetValue(formField.ParentObject) as ICollection<string> ?? []);
                    }
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (args.PropertyName == formField.PropertyName)
                        {
                            control.SetValue(formField.GetValue(formField.ParentObject) as ICollection<string> ?? []);    
                        }
                        else
                        {
                            control.SetOptions(optionsProperty.GetValue(formField.ParentObject) as ICollection<string> ?? []);
                        }
                    });
                }
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

            if (attributes.Alignment != DynamicFormAlignment.Default)
            {
                control.HorizontalAlignment = attributes.Alignment switch
                {
                    DynamicFormAlignment.Right => HorizontalAlignment.Right,
                    DynamicFormAlignment.Left => HorizontalAlignment.Left,
                    DynamicFormAlignment.Center => HorizontalAlignment.Center,
                    DynamicFormAlignment.Stretch => HorizontalAlignment.Stretch,
                    _ => control.HorizontalAlignment
                };
            }

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
        else if (formField.Value is ICommand command)
        {
            var control = new Button() { Content = attributes.ButtonText };
            
            if (attributes.Alignment != DynamicFormAlignment.Default)
            {
                control.HorizontalAlignment = attributes.Alignment switch
                {
                    DynamicFormAlignment.Right => HorizontalAlignment.Right,
                    DynamicFormAlignment.Left => HorizontalAlignment.Left,
                    DynamicFormAlignment.Center => HorizontalAlignment.Center,
                    DynamicFormAlignment.Stretch => HorizontalAlignment.Stretch,
                    _ => control.HorizontalAlignment
                };
            }
            
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