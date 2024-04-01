using DynamicForms.Example.Shared;

namespace DynamicForms.Example.WPF;

public class MainWindowViewModel
{
    public BasicExample BasicExample { get; set; } = new();
    
    public FieldsExample FieldsExample { get; set; } = new();
    
    public GroupsExample GroupsExample { get; set; } = new();
    
    public ChildObjectExample ChildObjectExample { get; set; } = new();
    
    public ReorderExample ReorderExample { get; set; } = new();
}