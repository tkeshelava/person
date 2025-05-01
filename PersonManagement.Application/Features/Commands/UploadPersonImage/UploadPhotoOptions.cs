using Microsoft.VisualBasic.CompilerServices;

namespace PersonManagment.Application.Features.Commands.UploadPersonImage;

public class UploadPhotoOptions
{
    public string UploadPath { get; set; }
    public long MaxSize { get; set; }
    public string[] SupportedFormats { get; set; }
}