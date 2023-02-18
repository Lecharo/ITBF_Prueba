using APIDevDailyActivities;
using APIDevDailyActivities.Data;
using APIDevDailyActivities.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();
logger.Debug("Init main in Program.cs");

try
{
    var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Nuevo servicio conectar a la BD SQLite, el connectionString esta en appSettings.json
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    // AutoMapper
    IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
    builder.Services.AddScoped<IDailyActivityRepository, DailyActivityRepository>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<ILaborRepository, LaborRepository>();

    // enable CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Use CORS
    app.UseCors(myAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Program.cs has stopped because there was an exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
