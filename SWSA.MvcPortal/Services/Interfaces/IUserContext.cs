namespace SWSA.MvcPortal.Services.Interfaces;

public interface IUserContext
{
    string Name { get; }    
    string StaffId { get; }

    DateTime LoginTime { get; }
    int EntityId { get; }
}
