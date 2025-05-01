using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PersonManagment.Application.Resources;

namespace PersonManagment.Application.Features.Commands.UploadPersonImage;

public class UploadPersonImageCommandValidator : AbstractValidator<UploadPersonImageCommand>
{
    private UploadPhotoOptions uploadPhotoOptions;

    public UploadPersonImageCommandValidator(IOptionsSnapshot<UploadPhotoOptions> uploadPhotoOptionsSnapshot)
    {
        uploadPhotoOptions = uploadPhotoOptionsSnapshot?.Value ??
                             throw new ArgumentNullException(nameof(uploadPhotoOptionsSnapshot));

        RuleFor(x => x.Image)
            .NotNull()
            .WithMessage(ErrorMessages.ImageFileRequired)
            .Must(IsValidImageExtension)
            .WithMessage(ErrorMessages.InvalidImageFileType)
            .Must(IsValidFileSize)
            .WithMessage(ErrorMessages.ImageFileSizeExceeded);
    }

    private bool IsValidImageExtension(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return uploadPhotoOptions.SupportedFormats.Contains(fileExtension);
    }

    private bool IsValidFileSize(IFormFile file)
    {
        return file.Length <= uploadPhotoOptions.MaxSize;
    }
}