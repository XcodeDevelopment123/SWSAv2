using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.ScheduledJobs.Profiles;

public class ScheduledJobProfile : Profile
{

    public ScheduledJobProfile()
    {

        CreateMap<ScheduledJob, ScheduledJob>();
        CreateMap<ScheduledJob, ScheduledJobVM>()
                  .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom((src, dest) =>
                  {
                      if (src.User != null)
                      {
                          return src.User.FullName;
                      }
                      return src.CreatedBy;
                  }))
                  .ForMember(dest => dest.StaffId, opt => opt.MapFrom((src, dest) => src.User?.StaffId ?? ""))
                  .ForMember(dest => dest.IsRequireExtraParams, opt => opt.MapFrom(src => src.RequiresExtraPayload()))
                  .ForMember(dest => dest.PayloadJson, opt => opt.MapFrom(src => src.RequestPayloadJson))
                  .AfterMap((src, dest) =>
                  {
                      dest.CronFields = dest.ParseCron();
                  });
    }

}
