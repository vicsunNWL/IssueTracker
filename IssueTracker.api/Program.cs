using IssueTracker.api.Endpoints;
using IssueTracker.api.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<TicketValidationService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapTicketEndpoints();

app.Run();