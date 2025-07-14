using AutoMapper;
using Microsoft.Extensions.Options;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Dtos.Requests.DocumentRecords;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.DocumentRecords.Profiles;

public class DocumentRecordProfile : Profile
{
    public DocumentRecordProfile() { }

    public DocumentRecordProfile(IOptions<FileSettings> fileSettings)
    {
        CreateMap<DocumentRecord, DocumentRecord>();
        CreateMap<DocumentRecord, DocumentRecordVM>()
              .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.StaffName, opt => opt.MapFrom((src, dest) => src.HandledByStaff?.FullName ?? AppSettings.NotAvailable))
              .ForMember(dest => dest.HandledByStaffId, opt => opt.MapFrom((src, dest) => src.HandledByStaff?.StaffId ?? src.HandledByStaffId.ToString()))
              .ForMember(dest => dest.FlowType, opt => opt.MapFrom((src, dest) => src.DocumentFlow))
              .ForMember(dest => dest.CompanyId, opt => opt.MapFrom((src, dest) => src.ClientId))
              .ForMember(dest => dest.CompanyName, opt => opt.MapFrom((src, dest) => src.Client.Name))
              .ForMember(dest => dest.FlowType, opt => opt.MapFrom((src, dest) => src.DocumentFlow));
           

        //Only for map link
        CreateMap<DocumentRecordVM, DocumentRecordVM>()
            .ForMember(dest => dest.AttachmentPath, opt => opt.MapFrom((src, dest) =>
                {
                    if (string.IsNullOrEmpty(src.AttachmentPath))
                        return null!;

                    var localDomain = fileSettings.Value.LocalDomain;
                    return src.AttachmentPath.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? src.AttachmentPath
                : $"{localDomain}{src.AttachmentPath}";
                }))
              .ForMember(dest => dest.DownloadLink, opt => opt.MapFrom((src, dest) =>
              {
                  if (string.IsNullOrEmpty(src.AttachmentPath))
                      return null!;

                  if (src.AttachmentPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                      return src.AttachmentPath;

                  //file controller and download link
                  var downloadPath = $"/files/download?path={Uri.EscapeDataString(src.AttachmentPath)}";
                  if (!string.IsNullOrEmpty(src.AttachmentFileName))
                  {
                      downloadPath += $"&fileOriName={Uri.EscapeDataString(src.AttachmentFileName)}";
                  }
                  var localDomain = fileSettings.Value.LocalDomain;
                  return $"{localDomain}{downloadPath}";
              }))
               .ForMember(dest => dest.ViewLink, opt => opt.MapFrom((src, dest) =>
               {
                   if (string.IsNullOrEmpty(src.AttachmentPath))
                       return null!;

                   if (src.AttachmentPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                       return src.AttachmentPath;

                   //file controller and download link
                   var downloadPath = $"/files/view?path={Uri.EscapeDataString(src.AttachmentPath)}";
                   var localDomain = fileSettings.Value.LocalDomain;
                   return $"{localDomain}{downloadPath}";
               }));


        CreateMap<DocumentRecordRequest, DocumentRecord>()
            .ForMember(dest => dest.HandledByStaffId, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentFlow, opt => opt.MapFrom(src => src.FlowType));
    }

}