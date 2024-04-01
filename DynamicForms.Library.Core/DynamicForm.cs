namespace DynamicForms.Library.Core;

public class DynamicForm
{
    public DynamicForm(object parentObject)
    {
        ParentGroup = new DynamicFormGroup(parentObject, "");
    }
    
    public DynamicFormGroup ParentGroup { get; init; }
}