using Microsoft.EntityFrameworkCore;
using Swexato.Api.Data;
using Swexato.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("CONN_STRING")
    ?? "Host=localhost;Port=5432;Database=swexato;Username=swexato;Password=swexato";

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(conn));

builder.Services.AddScoped<PessoaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
