using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Persistence.QueryExtensions;

public static class ClientQueryExtensions
{
    public static IQueryable<BaseCompany> ApplyFilter(this IQueryable<BaseCompany> query, ClientFilterRequest req)
    {
        if (!string.IsNullOrEmpty(req.Grouping))
        {
            query = query.Where(c => c.Group != null && c.Group.Contains(req.Grouping));
        }

        if (!string.IsNullOrEmpty(req.Referral))
        {
            query = query.Where(c => c.Referral != null && c.Referral.Contains(req.Referral));
        }

        if (!string.IsNullOrEmpty(req.FileNo))
        {
            query = query.Where(c => c.FileNo != null && c.FileNo.Contains(req.FileNo));
        }

        if (!string.IsNullOrEmpty(req.Name))
        {
            query = query.Where(c => c.Name != null && c.Name.Contains(req.Name));
        }

        if (!string.IsNullOrEmpty(req.CompanyNumber))
        {
            query = query.Where(c => c.RegistrationNumber != null && c.RegistrationNumber.Contains(req.CompanyNumber));
        }

        if (req.IncorpDateFrom.HasValue)
        {
            query = query.Where(x => x.IncorporationDate >= req.IncorpDateFrom.Value);
        }

        if (req.IncorpDateTo.HasValue)
        {
            query = query.Where(x => x.IncorporationDate <= req.IncorpDateTo.Value);
        }

        return query;
    }

    public static Task<bool> ExistsAsync(this IQueryable<BaseClient> query, int id)
    {
        return query.AnyAsync(c => c.Id == id);
    }

    public static Task<bool> CompanyExistsAsync(this IQueryable<BaseCompany> query, string regNo, string name)
    {
        return query.AnyAsync(c =>
            c.RegistrationNumber == regNo &&
            c.Name == name);
    }

    public static Task<bool> CompanyExistsAsync(this IQueryable<BaseCompany> query, int id, string regNo, string name)
    {
        return query.AnyAsync(c =>
            c.RegistrationNumber == regNo &&
            c.Name == name && c.Id != id);
    }

    #region Individual Client
    public static Task<bool> IcOrPassportExistsAsync(this IQueryable<IndividualClient> query, string icOrPassport)
    {
        return query.AnyAsync(c =>
            c.ICOrPassportNumber == icOrPassport);
    }

    public static Task<bool> IcOrPassportExistsAsync(this IQueryable<IndividualClient> query, int id, string icOrPassport)
    {
        return query.AnyAsync(c =>
            c.ICOrPassportNumber == icOrPassport && c.Id != id);
    }

    public static IQueryable<IndividualClient> ApplyFilter(this IQueryable<IndividualClient> query, ClientFilterRequest req)
    {
        if (!string.IsNullOrEmpty(req.Grouping))
        {
            query = query.Where(c => c.Group != null && c.Group.Contains(req.Grouping));
        }

        if (!string.IsNullOrEmpty(req.Referral))
        {
            query = query.Where(c => c.Referral != null && c.Referral.Contains(req.Referral));
        }

        if (!string.IsNullOrEmpty(req.Name))
        {
            query = query.Where(c => c.Name != null && c.Name.Contains(req.Name));
        }

        if (!string.IsNullOrEmpty(req.Profession))
        {
            query = query.Where(c => c.Profession != null && c.Profession.Contains(req.Profession));
        }

        return query;
    }
    #endregion


}
