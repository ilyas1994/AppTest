using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestApp.AppDbContext;
using TestApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Mapper

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#region Dynamic DI
// DI DYNAMIC CREATED 
var assemblyLogicCurrency = Assembly.Load("LogicCurrency");
var assemblyTestApp = Assembly.Load("TestApp");

var typesLogicCurrency = assemblyLogicCurrency.GetTypes()
    .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && type.Name.EndsWith("_Currency_DI"))
    .ToList();

var typesTestApp = assemblyTestApp.GetTypes()
    .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && type.Name.EndsWith("_TestApp_DI"))
    .ToList();

var types = typesLogicCurrency.Concat(typesTestApp).ToList();

foreach (var type in types)
{
    var interfaces = type.GetInterfaces();
    foreach (var @interface in interfaces)
    {
        builder.Services.AddTransient(@interface, type);
    }
}
#endregion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();


// --------------------

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
