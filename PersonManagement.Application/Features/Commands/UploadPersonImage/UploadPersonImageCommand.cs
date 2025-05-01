using MediatR;
using Microsoft.AspNetCore.Http;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;

namespace PersonManagment.Application.Features.Commands.UploadPersonImage;

public record UploadPersonImageCommand(int Id, IFormFile Image) : IRequest;

public class UploadPersonImageCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IStorageClient storageClient) : IRequestHandler<UploadPersonImageCommand>
{
    public async Task Handle(UploadPersonImageCommand command, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(command.Id);
        if (person == null)
        {
            throw new KeyNotFoundException("Person not found.");
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(command.Image.FileName)}";
        
        using (var stream = command.Image.OpenReadStream())
        {
            string fileUrl = await storageClient.UploadFileAsync(stream, fileName, cancellationToken);
            
            person.ImageUrl = fileUrl;
        }
        
        if(person.ImageUrl != null)
            

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}