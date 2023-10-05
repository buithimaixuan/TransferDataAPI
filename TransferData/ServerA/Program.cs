using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ServerA.Data;
using ServerA.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add DbContext
builder.Services.AddDbContext<AppDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ServerA"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<FacilityService>();
builder.Services.AddTransient<ResidentService>();
builder.Services.AddTransient<ProgressNoteService>();


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

