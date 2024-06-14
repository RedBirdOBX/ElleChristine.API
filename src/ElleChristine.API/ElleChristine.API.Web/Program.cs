using ElleChristine.APi.Service;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

//--LOGGING--//
//Log.Logger = new LoggerConfiguration()
//                    .MinimumLevel.Information()
//                    .WriteTo.MSSqlServer
//                    (
//                        connectionString: builder.Configuration["ConnectionStrings:sqlServerDbConnectionString"],
//                        sinkOptions: new MSSqlServerSinkOptions
//                        {
//                            TableName = "Logs",
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


string connectionString = builder.Configuration["ConnectionStrings:postgresDbConnectionString"];

string tableName = "logs";

IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
    { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
    { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
    { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
    { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
    { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
};

var logger = new LoggerConfiguration()
    .WriteTo.PostgreSQL(connectionString, tableName, columnWriters)
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)   
    .CreateLogger();

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

// custom services: inject interfaceX, provide an implementation of concrete type Y
//builder.Services.AddDbContext<ElleChristineDbContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:sqlServerDbConnectionString"]));

builder.Services.AddDbContext<ElleChristineDbContext>(dbContextOptions => dbContextOptions.UseNpgsql(builder.Configuration["ConnectionStrings:postgresDbConnectionString"]));
builder.Services.AddScoped<IElleChristineDbRepository, ElleChristineDbRepository>();
builder.Services.AddScoped<IShowProcessor, ShowProcessor>();
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
