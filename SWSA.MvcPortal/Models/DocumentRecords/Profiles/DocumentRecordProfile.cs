using AutoMapper;
using Microsoft.Extensions.Options;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.DocumentRecords.Profiles;

public class DocumentRecordProfile : Profile
{
    public DocumentRecordProfile() { }

    public DocumentRecordProfile(IOptions<FileSettings> fileSettings)
    {
        CreateMap<DocumentRecord, DocumentRecordVM>()
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
                   var downloadPath = $"/file/download?path={Uri.EscapeDataString(src.AttachmentFilePath)}";
                   var localDomain = fileSettings.Value.LocalDomain;
                   return $"{localDomain}{downloadPath}";
               }));

    }

}