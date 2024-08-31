using Alicunde.System.Exam.Api.Extensions;
using Alicunde.System.Exam.Services.Bank.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDataBaseContext(builder.Configuration);
builder.Services.RegisterHttpClients(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterRepositories();
builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBankCommand).Assembly)
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();