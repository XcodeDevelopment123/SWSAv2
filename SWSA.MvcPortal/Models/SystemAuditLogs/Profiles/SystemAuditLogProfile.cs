using AutoMapper;
using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Systems;

namespace SWSA.MvcPortal.Models.SystemAuditLogs.Profiles;

public class SystemAuditLogProfile : Profile
{

    public SystemAuditLogProfile()
    {

        CreateMap<SystemAuditLog, SystemAuditLogListVM>()
            .ForMember(dest => dest.LogId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ActionType, opt => opt.MapFrom((src, dest) =>
            {
                return Enum.TryParse<SystemAuditActionType>(src.ActionType, true, out var result)
                        ? result
                        : SystemAuditActionType.Unknown;
            }))
            ;

        CreateMap<SystemAuditLog, SystemAuditLogVM>()
          .ForMember(dest => dest.LogId, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.Module, opt => opt.MapFrom((src, dest) =>
          {
              return Enum.TryParse<SystemAuditModule>(src.Module, true, out var result)
                      ? result
                      : SystemAuditModule.Unknown;
          }))
          .ForMember(dest => dest.ActionType, opt => opt.MapFrom((src, dest) =>
          {
              return Enum.TryParse<SystemAuditActionType>(src.ActionType, true, out var result)
                      ? result
                      : SystemAuditActionType.Unknown;
          }))
          .ForMember(dest => dest.ChangeSummaries, opt => opt.MapFrom((src, dest) =>
          {
              return JsonConvert.DeserializeObject<List<ChangeSummaryVM>>(src.ChangeSummaryJson ?? "[]");
          }))
         ;
    }
}
