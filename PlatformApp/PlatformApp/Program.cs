using Contracts;
using Microsoft.EntityFrameworkCore;
using PlatformApp.Models.DTO;
using PlatformApp.Repository;
using PlatformApp.Repository.DA;
using PlatformApp.Repository.DA.GamesLibrary;
using PlatformApp.Repository.Service;
using PlatformApp.Repository.Service.GamesLibrary;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddDbContext<PlatformDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PlatformConnString")));


builder.Services.AddScoped<IMappingDTO, MappingDTO>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IPlatformTypeRepository, PlatformTypeRepository>();
builder.Services.AddScoped<IPlatformGameRepository, PlatformGameRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddMassTransitWithRabbitMQ();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlatformDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
