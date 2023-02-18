using APPLogin;
using APPLogin.Data;
using APPLogin.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();
logger.Debug("Init main in Program.cs");

try
{
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
    //IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
    //builder.Services.AddSingleton(mapper);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IUserRepository, UserRepository>();

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
