namespace SWSA.MvcPortal.Commons.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class SystemAuditLogAttribute : Attribute
{
    public string Name { get; }

    public SystemAuditLogAttribute(string name)
    {
        Name = name;
    }
}