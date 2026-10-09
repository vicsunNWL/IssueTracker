using Dapper;
using Microsoft.Data.SqlClient;
using IssueTracker.api.Models;
using IssueTracker.api.Dtos;

namespace IssueTracker.api.Services;

public class TicketService
{
    private readonly string _connectionString;

    public TicketService(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("IssueTracker")
            ?? throw new InvalidOperationException(
                "Connection string 'IssueTracker' saknas.");
    }

    public async Task<IEnumerable<Ticket>> GetTickets()
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = """
            SELECT Id, Title, Description, Status, Priority
            FROM dbo.Tickets
            ORDER BY Id;
            """;

        return await connection.QueryAsync<Ticket>(sql);
    }

    public async Task<int> CreateTicket(CreateTicketDto ticket)
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = """
        INSERT INTO dbo.Tickets (Title, Description, Status, Priority)
        OUTPUT INSERTED.Id
        VALUES (@Title, @Description, 'Open', @Priority);
        """;

        return await connection.QuerySingleAsync<int>(sql, new
        {
            ticket.Title,
            ticket.Description,
            ticket.Priority
        });
    }
}
