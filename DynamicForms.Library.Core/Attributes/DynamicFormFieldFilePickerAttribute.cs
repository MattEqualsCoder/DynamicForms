namespace DynamicForms.Library.Core.Attributes;

/// <summary>
/// Attribute for creating a control for selecting a file or folder
/// </summary>
/// <param name="filePickerType">Whether the file picker should be for opening/saving files or a folder</param>
/// <param name="filter">The filter to use for selectable files. Uses the WPF file filter format.</param>
/// <param name="checkSum">MD5 checksum to use to validate if the uesr selected the correct file</param>
/// <param name="checkSumError">Error warning to display if the MD5 checksum is incorrect</param>
/// <param name="labelText">The form label text</param>
/// <param name="toolTipText">Text to display when hovering over the object</param>
/// <param name="visibleWhenProperty">Property to look at to determine if the field should be shown or not</param>
/// <param name="editableWhenProperty">Property to look at to determine if the field should be editable or not</param>
/// <param name="groupName">The group the field should be on</param>
/// <param name="order">The order to show the field in</param>
[AttributeUsage(AttributeTargets.Property)]
public class DynamicFormFieldFilePickerAttribute(
    FilePickerType filePickerType,
    string filter = "All files (*.*)|*.*",
    string? checkSum = null,
    string? checkSumError = null,
    string labelText = "",
    string? toolTipText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, toolTipText, visibleWhenProperty, editableWhenProperty, groupName, order)
{
    public override DynamicFormFieldType FieldType => DynamicFormFieldType.FilePicker;
    
    public FilePickerType FilePickerType { get; } = filePickerType;

    public string Filter { get; } = filter;

    public string? CheckSum { get; } = checkSum;

    public string? CheckSumError { get; } = checkSumError;
}

public enum FilePickerType
{
    OpenFile,
    SaveFile,
    Folder
}