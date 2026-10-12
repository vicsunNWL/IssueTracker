using IssueTracker.api.Dtos;
using IssueTracker.api.Services;

namespace IssueTracker.api.Endpoints;

public static class TicketEndpoint
{
    public static void MapTicketEndpoints(this WebApplication app)
    {
        app.MapGet("/api/tickets", async (TicketService service) =>
        {
            var tickets = await service.GetTickets();

            var result = tickets.Select(ticket => new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority
            });

            return Results.Ok(result);
        });

        app.MapPost("/api/tickets", async (
        CreateTicketDto ticket,
        TicketService service,
        TicketValidationService validationService) =>
        {
            var errors = validationService.Validate(ticket);

            if (errors.Count > 0)
            {
                return Results.ValidationProblem(errors);
            }

            var id = await service.CreateTicket(ticket);

            var result = new TicketDto
            {
                Id = id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = "Open",
                Priority = ticket.Priority
            };

            return Results.Created($"/api/tickets/{id}", result);
        });

        app.MapDelete("/api/tickets/{id:int}", async (
        int id,
        TicketService service) =>
        {
            var deleted = await service.DeleteTicket(id);

            if (!deleted)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        app.MapPut("/api/tickets/{id:int}", async (
    int id,
    UpdateTicketDto ticket,
    TicketService service,
    TicketValidationService validationService) =>
        {
            var errors = validationService.Validate(ticket);

            if (errors.Count > 0)
            {
                return Results.ValidationProblem(errors);
            }

            var updated = await service.UpdateTicket(id, ticket);

            if (!updated)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

    }

    


}