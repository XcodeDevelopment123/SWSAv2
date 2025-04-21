using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.ScheduledJobs.Profiles;


public class ScheduledJobProfile : Profile
{

    public ScheduledJobProfile()
    {

        CreateMap<ScheduledJob, ScheduledJobVM>()
                  .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom((src, dest) =>
                  {
                      if (src.User != null)
                      {
                          return src.User.FullName;
                      }
                      return src.CreatedBy;
                  }))
                    .ForMember(dest => dest.StaffId, opt => opt.MapFrom((src, dest) => src.User?.StaffId ?? "")
                  );




    }

}
