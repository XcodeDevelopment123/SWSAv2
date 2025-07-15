using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Services.Clients;

public interface IClientCreationFactory
{
    BaseCompany CreateCompanyAsync(CreateCompanyRequest req);
    IndividualClient CreateIndividualAsync(CreateIndividualRequest req);
}

public class ClientCreationFactory : IClientCreationFactory
{
    public BaseCompany CreateCompanyAsync(CreateCompanyRequest req)
    {
        var now = DateTime.Now;

        var msicCode = req.MsicCodeIds.Select(c => new CompanyMsicCode(c)).ToList();

        return req.ClientType switch
        {
            ClientType.SdnBhd => new SdnBhdClient
            {
                Group = req.CategoryInfo?.Group,
                Referral = req.CategoryInfo?.Referral,
                FileNo = req.CategoryInfo?.FileNo,

                Name = req.CompanyName,
                YearEndMonth = req.YearEndMonth,
                RegistrationNumber = req.RegistrationNumber,
                IncorporationDate = req.IncorporationDate,
                EmployerNumber = req.EmployerNumber,
                TaxIdentificationNumber = req.TaxIdentificationNumber,
                CompanyType = req.CompanyType,
                ClientType = req.ClientType,
                MsicCodes = msicCode
            },

            ClientType.Enterprise => new EnterpriseClient
            {
                Group = req.CategoryInfo?.Group,
                Referral = req.CategoryInfo?.Referral,
                FileNo = req.CategoryInfo?.FileNo,

                Name = req.CompanyName,
                YearEndMonth = req.YearEndMonth,
                RegistrationNumber = req.RegistrationNumber,
                IncorporationDate = req.IncorporationDate,
                EmployerNumber = req.EmployerNumber,
                TaxIdentificationNumber = req.TaxIdentificationNumber,
                CompanyType = req.CompanyType,
                ClientType = req.ClientType,
                MsicCodes = msicCode
            },

            ClientType.LLP => new LLPClient
            {
                Group = req.CategoryInfo?.Group,
                Referral = req.CategoryInfo?.Referral,
                FileNo = req.CategoryInfo?.FileNo,

                Name = req.CompanyName,
                YearEndMonth = req.YearEndMonth,
                RegistrationNumber = req.RegistrationNumber,
                IncorporationDate = req.IncorporationDate,
                EmployerNumber = req.EmployerNumber,
                TaxIdentificationNumber = req.TaxIdentificationNumber,
                CompanyType = req.CompanyType,
                ClientType = req.ClientType,
                MsicCodes = msicCode
            },

            _ => throw new NotSupportedException($"Unsupported company type: {req.CompanyType}")
        };
    }

    public IndividualClient CreateIndividualAsync(CreateIndividualRequest req)
    {
        return new IndividualClient
        {
            Group = req.CategoryInfo?.Group,
            Referral = req.CategoryInfo?.Referral,

            Name = req.IndividualName,
            YearEndMonth = req.YearEndMonth,
            ICOrPassportNumber = req.ICOrPassportNumber,
            Profession = req.Professions,
            TaxIdentificationNumber = req.TaxIdentificationNumber,
            ClientType = req.ClientType,
        };
    }
}
