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
              .ForMember(dest => dest.AttachmentPath, opt => opt.MapFrom((src, dest) =>
              {
                  if (string.IsNullOrEmpty(src.AttachmentFilePath))
                      return null!;

                  var localDomain = fileSettings.Value.LocalDomain;
                  return src.AttachmentFilePath.StartsWith("http", StringComparison.OrdinalIgnoreCase)
              ? src.AttachmentFilePath
              : $"{localDomain}{src.AttachmentFilePath}";
              }))
              .ForMember(dest => dest.DownloadLink, opt => opt.MapFrom((src, dest) =>
               {
                   if (string.IsNullOrEmpty(src.AttachmentFilePath))
                       return null!;

                   if (src.AttachmentFilePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                       return src.AttachmentFilePath;

                   //file controller and download link
                   var downloadPath = $"/files/download?path={Uri.EscapeDataString(src.AttachmentFilePath)}";
                   if (!string.IsNullOrEmpty(src.AttachmentFileName))
                   {
                       downloadPath += $"&fileOriName={Uri.EscapeDataString(src.AttachmentFileName)}";
                   }
                   var localDomain = fileSettings.Value.LocalDomain;
                   return $"{localDomain}{downloadPath}";
               }))
               .ForMember(dest => dest.ViewLink, opt => opt.MapFrom((src, dest) =>
               {
                   if (string.IsNullOrEmpty(src.AttachmentFilePath))
                       return null!;

                   if (src.AttachmentFilePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                       return src.AttachmentFilePath;

                   //file controller and download link
                   var downloadPath = $"/files/view?path={Uri.EscapeDataString(src.AttachmentFilePath)}";
                   var localDomain = fileSettings.Value.LocalDomain;
                   return $"{localDomain}{downloadPath}";
               }));

        CreateMap<DocumentRecordRequest, DocumentRecord>()
            .ForMember(dest => dest.HandledByStaffId, opt => opt.Ignore());
    }

}