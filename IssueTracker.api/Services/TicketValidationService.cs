using IssueTracker.api.Dtos;

namespace IssueTracker.api.Services;

public class TicketValidationService
{
    public Dictionary<string, string[]> Validate(CreateTicketDto ticket)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(ticket.Title))
        {
            errors["title"] = ["Titel får inte vara tom eller bara innehålla blanksteg."];
        }
        else if (ticket.Title.Length < 5 || ticket.Title.Length > 100)
        {
            errors["title"] = ["Titel måste vara mellan 5 och 100 tecken."];
        }

        if (string.IsNullOrWhiteSpace(ticket.Description))
        {
            errors["description"] =
                ["Beskrivning får inte vara tom eller bara innehålla blanksteg."];
        }
        else if (ticket.Description.Length < 10 || ticket.Description.Length > 1000)
        {
            errors["description"] =
                ["Beskrivning måste vara mellan 10 och 1000 tecken."];
        }

        if (ticket.Priority is not ("Low" or "Normal" or "High"))
        {
            errors["priority"] = ["Prioritet måste vara Low, Normal eller High."];
        }

        return errors;
    }

    public Dictionary<string, string[]> Validate(UpdateTicketDto ticket)
    {
        var errors = Validate(new CreateTicketDto
        {
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority
        });

        if (ticket.Status is not ("Open" or "InProgress" or "Closed"))
        {
            errors["status"] = ["Status måste vara Open, InProgress eller Closed."];
        }

        return errors;
    }
}