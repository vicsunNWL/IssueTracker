using Dapper;
using Microsoft.Data.SqlClient;
using IssueTracker.api.Models;

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
}
