using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using DynamicForms.Library.Core.Attributes;

namespace DynamicForms.Library.WPF.Fields;

public partial class DynamicFormFilePicker : UserControl
{
    private FilePickerType _filePickerType;
    private string _filter;
    private string? _checkSum;
    private string? _checkSumError;
    
    public DynamicFormFilePicker(DynamicFormFieldFilePickerAttribute attributes, string path)
    {
        InitializeComponent();
        SetValue(path);
        _filePickerType = attributes.FilePickerType;
        _filter = attributes.Filter;
        _checkSum = attributes.CheckSum;
        _checkSumError = attributes.CheckSumError;
    }

    public string Value { get; private set; } = "";
    
    public event EventHandler? ValueChanged;

    public void SetValue(string path)
    {
        Value = path;
        MainTextBox.Text = path;
    }

    private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_filePickerType == FilePickerType.OpenFile)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = Value;
            dialog.Filter = _filter;
            bool? result = dialog.ShowDialog();
            if (result == true && VerifyHash(dialog.FileName))
            {
                SetValue(dialog.FileName);
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else if (_filePickerType == FilePickerType.SaveFile)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = Value;
            dialog.Filter = _filter;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                SetValue(dialog.FileName);
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else if (_filePickerType == FilePickerType.Folder)
        {
            var dialog = new Microsoft.Win32.OpenFolderDialog();
            dialog.FolderName = Value;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                SetValue(dialog.FolderName);
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool VerifyHash(string file)
    {
        if (string.IsNullOrEmpty(_checkSum))
        {
            return true;
        }
        
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(file);
        var hash = md5.ComputeHash(stream);
        var hashString = BitConverter.ToString(hash).Replace("-", "");

        if (_checkSum.Equals(hashString, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        
        var error = string.IsNullOrEmpty(_checkSumError)
            ? "Selected file does not match expected hash. Do you still want to select the file?"
            : _checkSumError;

        var result = MessageBox.Show(error, "Validation Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
        return result == MessageBoxResult.Yes;
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        SetValue("");
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }
}