namespace SWSA.MvcPortal.Commons.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ModuleAttribute : Attribute
{
    public string Name { get; }

    public ModuleAttribute(string name)
    {
        Name = name;
    }
}
