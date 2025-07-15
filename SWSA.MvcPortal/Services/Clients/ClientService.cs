using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Commons.Guards;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Dtos.Responses.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Services.Clients;

public class ClientService(
   IClientRepository _repo,
   IClientCreationFactory _clientCreationFactory
    ) : IClientService
{

    public async Task<IEnumerable<object>> SearchClientsAsync(ClientType type, ClientFilterRequest request)
    {
        return type switch
        {
            ClientType.SdnBhd => await GetFilteredCompanyClientsAsync<SdnBhdClient>(request),
            ClientType.LLP => await GetFilteredCompanyClientsAsync<LLPClient>(request),
            ClientType.Individual => await GetFilteredIndividualClientsAsync(request),
            ClientType.Enterprise => await GetFilteredCompanyClientsAsync<EnterpriseClient>(request),
            _ => throw new ArgumentException($"Unsupported client type: {type}")
        };
    }

    public async Task<List<T>> GetClientsAsync<T>() where T : BaseClient
    {
        var data = await _repo.Query().OfType<T>().ToListAsync();

        return data;
    }

    public async Task<BaseClient> GetClientByIdAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id);
        Guard.AgainstNullData(data, "Client Not Found");

        return data!;
    }


    public async Task<BaseCompany> CreateCompanyAsync(CreateCompanyRequest req)
    {
        var isExist = await _repo.CompanyExists(req.RegistrationNumber, req.CompanyName);
        if (isExist)
        {
            throw new BusinessLogicException("Company Name / Number already exists");
        }

        var entity = _clientCreationFactory.CreateCompanyAsync(req);
        _repo.Add(entity);
        await _repo.SaveChangesAsync();

        return entity;
    }

    public async Task<IndividualClient> CreateIndividualAsync(CreateIndividualRequest req)
    {
        var isExist = await _repo.CompanyExists(req.IndividualName, req.ICOrPassportNumber);
        if (isExist)
        {
            throw new BusinessLogicException("IC/Passport already exists");
        }

        var entity = _clientCreationFactory.CreateIndividualAsync(req);
        _repo.Add(entity);
        await _repo.SaveChangesAsync();
        return new();
    }

    private async Task<IEnumerable<T>> GetFilteredCompanyClientsAsync<T>(ClientFilterRequest req) where T : BaseCompany
    {
        var query = _repo.Query().OfType<T>();

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

        return await query.ToListAsync();
    }

    private async Task<IEnumerable<IndividualClient>> GetFilteredIndividualClientsAsync(ClientFilterRequest req)
    {
        var query = _repo.Query().OfType<IndividualClient>();

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

        return await query.ToListAsync();
    }

}
