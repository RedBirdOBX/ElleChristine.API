using ElleChristine.APi.Service;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;
using Serilog.Configuration;
using ElleChristine.API.Web.Logging.Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

//--LOGGING--//
var logTblName = "logs";
var connString = builder.Configuration["ConnectionStrings:postgresDbConnectionString"];

// Serilog log w/ MS Sql Server //
//Log.Logger = new LoggerConfiguration()
//                    .MinimumLevel.Information()
//                    .WriteTo.MSSqlServer
//                    (
//                        connectionString: connString,
//                        sinkOptions: new MSSqlServerSinkOptions
//                        {
//                            TableName = logTblName,
//                            SchemaName = "dbo",
//                            AutoCreateSqlTable = true
//                        },
//                        restrictedToMinimumLevel: LogEventLevel.Information,
//                        formatProvider: null,
//                        columnOptions: null,
//                        logEventFormatter: null
//                    )
//                    .WriteTo.Console()
//                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
//                .CreateLogger();
// END of Serilog log w/ MS Sql Server //


// Serilog log w/ Postgres //
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day).MinimumLevel.Debug()
	.WriteTo.PostgreSQL(connString, logTblName, SerilogConfig.ColumnWriters, LogEventLevel.Information, null, null, 1, 1, null, true, "", true, true)
	.CreateLogger();
// End of Serilog log w/ Postgres //



//--SERVICES--//
builder.Services.AddControllers(options =>
{
    //  media types: don't blindly return json regardless of what they asked for.
    options.ReturnHttpNotAcceptable = true;
})
.AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

// OpenAPI & Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen((setupAction) =>
{
    // since multiple projects will have xml documentation, we will need to loop thru all
    // the files and include all xml docs.
    DirectoryInfo baseDirectoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
    foreach (var fileInfo in baseDirectoryInfo.EnumerateFiles("ElleChristine.API*.xml"))
    {
        setupAction.IncludeXmlComments(fileInfo.FullName);
    };
});

// ms sql
//builder.Services.AddDbContext<ElleChristineDbContext>(dbContextOptions => dbContextOptions.UseSqlServer(connString));

// postgres
builder.Services.AddDbContext<ElleChristineDbContext>(dbContextOptions => dbContextOptions.UseNpgsql(connString));

builder.Services.AddScoped<IElleChristineDbRepository, ElleChristineDbRepository>();
builder.Services.AddScoped<IShowProcessor, ShowProcessor>();
builder.Services.AddScoped<IPhotoProcessor, PhotoProcessor>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddHealthChecks();

// auto-mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // remove if not wanted to expose in prod
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapHealthChecks("/api/health");

app.Run();
