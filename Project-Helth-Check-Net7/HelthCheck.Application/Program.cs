using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////ADICIONAR INCLUSÃO HEALTH
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString: builder.Configuration.GetConnectionString("ConnectionSql"), tags: new[] { "sql-sever" })
    .AddRedis(builder.Configuration.GetConnectionString("ConnectionCacheRedis"), tags: new[] { "reddis-server" });

//ADICIONANDO INTERFACE PARA O HEALTH CHECK
builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(2);
    opt.MaximumHistoryEntriesPerEndpoint(5);
    opt.AddHealthCheckEndpoint("Habilitado health Chcks", "/health");
}).AddInMemoryStorage();

var app = builder.Build();

//ROTA PARA EXIBIR O HELTH
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = p => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//ROTA PARA EXIBIR O HELTH COM INTERFACE
app.UseHealthChecksUI(opt =>
{
    opt.UIPath = "/dashboard";
});

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
