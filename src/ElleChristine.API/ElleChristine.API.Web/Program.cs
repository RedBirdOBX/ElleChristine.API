using ElleChristine.APi.Service;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

//--LOGGING--//
Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.MSSqlServer
                    (
                        connectionString: builder.Configuration["ConnectionStrings:elleChristineDbConnectionString"],
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Logs",
                            SchemaName = "dbo",
                            AutoCreateSqlTable = true
                        },
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        formatProvider: null,
                        columnOptions: null,
                        logEventFormatter: null
                    )
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
builder.Services.AddDbContext<ElleChristineDbContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:elleChristineDbConnectionString"]));
builder.Services.AddScoped<IElleChristineDbRepository, ElleChristineDbRepository>();
builder.Services.AddScoped<IShowProcessor, ShowProcessor>();

// sets content type to return based on file extension of file.
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0
builder.Services.AddHealthChecks();

// auto-mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();



// Configure the HTTP request pipeline.
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

app.UseAuthorization();

// required for endpoint routing. 'where an endpoint is EXECUTED'.
// EndPoint routing: preferred way for .NET 6
// mapping the URI of the request to a controller & action.
app.UseEndpoints(endpoints =>
{
    // adds/maps endpoints for our controller actions (with no conventions)
    endpoints.MapControllers();
});

app.MapHealthChecks("/api/health");

app.Run();
