using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ServerA.Data;
using ServerA.Data.Services;
using ServerA.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder => builder
            .WithOrigins("http://localhost:4200") // Allow requests from this origin
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IProgressNoteService, ProgressNoteService>();


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

app.UseCors("AllowLocalhost4200"); // Use the CORS policy defined above

app.UseAuthorization();

app.MapControllers();

app.Run();

