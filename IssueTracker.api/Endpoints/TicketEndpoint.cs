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
    }
}