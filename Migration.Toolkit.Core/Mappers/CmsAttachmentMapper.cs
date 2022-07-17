using CMS.Base;
using CMS.MediaLibrary;
using Microsoft.Extensions.Logging;
using Migration.Toolkit.Core.Abstractions;
using Migration.Toolkit.Core.Contexts;
using Migration.Toolkit.Core.Helpers;
using Migration.Toolkit.Core.MigrationProtocol;

namespace Migration.Toolkit.Core.Mappers;

public record CmsAttachmentMapperSource(KX13M.CmsAttachment Attachment, int TargetLibraryId, IUploadedFile File, string LibrarySubFolder);

public class CmsAttachmentMapper: EntityMapperBase<CmsAttachmentMapperSource, MediaFileInfo>
{
    public CmsAttachmentMapper(ILogger<CmsAttachmentMapper> logger, PrimaryKeyMappingContext pkContext, IProtocol protocol) : base(logger, pkContext, protocol)
    {
    }

    protected override MediaFileInfo? CreateNewInstance(CmsAttachmentMapperSource source, MappingHelper mappingHelper, AddFailure addFailure)
    {
        if (mappingHelper.TranslateRequiredId<KX13M.CmsSite>(s => s.SiteId, source.Attachment.AttachmentSiteId, out var siteId))
        {
            return new MediaFileInfo(source.File, source.TargetLibraryId, source.LibrarySubFolder, 0, 0, 0, siteId);
        }

        var error = HandbookReferences
            .FailedToCreateTargetInstance<MediaFileInfo>()
            .WithData(source);
            
        addFailure(new MapperResultFailure<MediaFileInfo>(error));
        return null;
    }

    protected override MediaFileInfo MapInternal(CmsAttachmentMapperSource args, MediaFileInfo target, bool newInstance, MappingHelper mappingHelper, AddFailure addFailure)
    {
        var (cmsAttachment, targetLibraryId, _, _) = args;
        
        target.FileName = Path.GetFileNameWithoutExtension(cmsAttachment.AttachmentName);
        target.FileTitle = cmsAttachment.AttachmentTitle ?? cmsAttachment.AttachmentName;
        target.FileDescription = cmsAttachment.AttachmentDescription ?? string.Empty;
        target.FileExtension = cmsAttachment.AttachmentExtension;
        target.FileMimeType = cmsAttachment.AttachmentMimeType;
        target.FileSize = cmsAttachment.AttachmentSize;
        target.FileImageWidth = cmsAttachment.AttachmentImageWidth ?? 0;
        target.FileImageHeight = cmsAttachment.AttachmentImageHeight ?? 0;
        target.FileGUID = cmsAttachment.AttachmentGuid;
        target.FileLibraryID = targetLibraryId;
        if (mappingHelper.TranslateRequiredId<KX13.Models.CmsSite>(s => s.SiteId, cmsAttachment.AttachmentSiteId, out var siteId))
        {
            target.FileSiteID = siteId;
        }
        
        // target.FileCreatedByUserID = cmsAttachment.?;
        // target.FileModifiedByUserID = cmsAttachment.?;
        // target.FileCreatedWhen = cmsAttachment.?;
        
        target.FileModifiedWhen = cmsAttachment.AttachmentLastModified;

        KenticoHelper.CopyCustomData(target.FileCustomData, cmsAttachment.AttachmentCustomData);
        
        return target;
    }
}