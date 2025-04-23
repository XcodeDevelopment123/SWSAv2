namespace SWSA.MvcPortal.Commons.Filters;

[AttributeUsage(AttributeTargets.Property)]
public class SystemAuditLogAttribute : Attribute
{
    public string Name { get; }

    public SystemAuditLogAttribute(string name)
    {
        Name = name;
    }
}