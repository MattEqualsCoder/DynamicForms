namespace DynamicForms.Library.Core.Attributes;

[AttributeUsage((AttributeTargets.Property))]
public class DynamicFormFieldFilePickerAttribute(
    FilePickerType filePickerType,
    string filter = "All Files:*",
    string? checkSum = null,
    string? checkSumError = null,
    string labelText = "",
    string? hintText = null,
    string? visibleWhenProperty = null,
    string? editableWhenProperty = null,
    string groupName = "",
    int order = int.MaxValue)
    : DynamicFormFieldAttribute(labelText, hintText, visibleWhenProperty, editableWhenProperty, groupName, order)
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