using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class ClientQueryExtensions
{
    public static Task<bool> CompanyExistsAsync(this IQueryable<BaseCompany> query, string regNo, string name)
    {
        return query.AnyAsync(c =>
            c.RegistrationNumber == regNo &&
            c.Name == name);
    }

    public static Task<bool> ExistsAsync(this IQueryable<IndividualClient> query, string icOrPassport)
    {
        return query.AnyAsync(c =>
            c.ICOrPassportNumber == icOrPassport);
    }
}
