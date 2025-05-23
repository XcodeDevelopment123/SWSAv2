using Mapster;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Commons.MapsterConfigs;

public class CompanyMapsterConfig : IMapsterConfig
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Company, CompanyListVM>()
              .Map(dest => dest.CompanyId, src => src.Id)
              .Map(dest => dest.CompanyName, src => src.Name)
              .Map(dest => dest.RegistrationNumber, src => src.RegistrationNumber)
              .Map(dest => dest.EmployerNumber, src => src.EmployerNumber)
              .Map(dest => dest.TaxIdentificationNumber, src => src.TaxIdentificationNumber)
              .Map(dest => dest.YearEndMonth, src => src.YearEndMonth)
              .Map(dest => dest.IncorporationDate, src => src.IncorporationDate)
              .Map(dest => dest.CompanyType, src => src.CompanyType)
              .Map(dest => dest.CompanyDirectorName,
                        src => src.Owners
                                 .Where(o => o.Position == PositionType.Director)
                                 .OrderBy(o => o.Id)
                                 .Select(o => o.NamePerIC)
                                 .FirstOrDefault() ?? "")
              .Map(dest => dest.ContactsCount,
                    src => src.CommunicationContacts.Count + src.OfficialContacts.Count)
              .Map(dest => dest.MsicCodesCount, src => src.MsicCodes.Count)
              .Map(dest => dest.WorkCount, src => src.WorkAssignments.Count);

        config.ForType<Company, CompanySimpleInfoVM>()
            .Map(dest => dest.CompanyId, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.RegistrationNumber, src => src.RegistrationNumber);

        config.ForType<Company, CompanySelectionVM>()
            .Map(dest => dest.CompanyId, src => src.Id);

        config.ForType<Company, CompanySecretaryVM>()
            .Map(dest => dest.CompanyId, src => src.Id)
            .Map(dest => dest.CompanyName, src => src.Name)
            .Map(dest => dest.WorkAssignments, src => src.WorkAssignments);
    }
}