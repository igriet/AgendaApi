using AgendaApi.Interface.Data;
using AgendaApi.Interface.Service;
using AgendaApi.Logging;
using AgendaApi.Repository;
using AgendaApi.Service;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddTransient<IAgendaService, AgendaService>();
builder.Services.AddTransient<IAgendaRepository, AgendaRepository>();
builder.Services.AddSingleton<ILogManager, NLogManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
