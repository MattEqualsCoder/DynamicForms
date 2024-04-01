using DynamicForms.Example.Shared;

namespace DynamicForms.Example.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public BasicExample BasicExample { get; set; } = new();
    
    public FieldsExample FieldsExample { get; set; } = new();
    
    public GroupsExample GroupsExample { get; set; } = new();
    
    public ChildObjectExample ChildObjectExample { get; set; } = new();
    
    public ReorderExample ReorderExample { get; set; } = new();
}