using ApiGraphQL.UtilGraphQL;
using ApiGraphQL.UtilGraphQL.Types;
using Contracts;
using DataRepository;
using DataRepository.Interface;
using MassTransit;
using MyGraphQLAPI.Repository.UtilGraphQL;


var corsName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors( options =>
{
options.AddPolicy(
    name: corsName,
    policy => {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();

    });
});

builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IJogoGeneroRepository, JogoGeneroRepository>();
builder.Services.AddScoped<IEstudioRepository, EstudioRepository>();

builder.Services.AddMassTransitWithRabbitMQ();
builder.Services.AddScoped(_ =>
    new DbContext(builder.Configuration.GetConnectionString("PostgresConnection"))
);


builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<JogoType>()
    .AddType<GeneroType>()
    .AddType<EstudioType>()
    .AddMutationType<Mutation>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();
app.MapGraphQL();
app.UseCors(corsName);
app.Run();
