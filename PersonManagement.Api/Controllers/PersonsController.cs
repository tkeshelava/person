using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Application.Features.Commands.AddConnectedPerson;
using PersonManagment.Application.Features.Commands.AddPerson;
using PersonManagment.Application.Features.Commands.DeleteConnectedPerson;
using PersonManagment.Application.Features.Commands.DeletePerson;
using PersonManagment.Application.Features.Commands.UpdatePerson;
using PersonManagment.Application.Features.Commands.UploadPersonImage;
using PersonManagment.Application.Features.Queries.GetPerson;
using PersonManagment.Application.Features.Queries.GetPersonList;
using PersonManagment.Application.Features.Queries.GetPersonsConnection;
using PersonManagment.Application.Models;

namespace PersonManagement.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] AddPersonCommand command)
    {
        await mediator.Send(command);
        return Created();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        var person = await mediator.Send(new GetPersonQuery(id));
        return Ok(person);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("The ID in the URL must match the ID in the request body.");
        }

        await mediator.Send(command);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        await mediator.Send(new DeletePersonCommand(id));
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetPersonList([FromQuery] GetPersonFilter filter)
    {
        var query = new GetPersonListQuery(filter);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("connections")]
    public async Task<IActionResult> GetPersonsConnection()
    {
        var result = await mediator.Send(new GetPersonsConnectionQuery());
        return Ok(result);
    }

    [HttpPost("{id}/connections")]
    public async Task<IActionResult> AddConnectedPerson([FromBody] AddConnectedPersonCommand command)
    {
        await mediator.Send(command);
        return Created();
    }

    [HttpDelete("{id}/connections/{connectedPersonId}")]
    public async Task<IActionResult> DeleteConnectedPerson(int id, int connectedPersonId)
    {
        await mediator.Send(new DeleteConnectedPersonCommand(id, connectedPersonId));
        return Ok();
    }

    [HttpPatch("{id}/image")]
    public async Task<IActionResult> UploadPersonImage([FromForm] UploadPersonImageCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}