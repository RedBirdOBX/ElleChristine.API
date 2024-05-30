using ElleChristine.APi.Service;
using ElleChristine.API.Data.DbContexts;
using ElleChristine.API.Data.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


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
    // TO DO: see if this works on Azure.
    DirectoryInfo baseDirectoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
    foreach (var fileInfo in baseDirectoryInfo.EnumerateFiles("ElleChristine.API*.xml"))
    {
        setupAction.IncludeXmlComments(fileInfo.FullName);
    };

    //// adds security scheme to api documentation and auth tool/button in Swashbuckle UI
    //setupAction.AddSecurityDefinition("DrummersDatabaseAPIAuth", new OpenApiSecurityScheme()
    //{
    //    // basic authentication, not OAuth2 or OpenAPI
    //    Type = SecuritySchemeType.Http,

    //    // also: apiKey, oauth2, openIdConnect
    //    Scheme = "Bearer",

    //    Description = "Input a valid token to access this API"
    //});

    // to ensure Swashbuckle UI sends bearer token
    //setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "DrummersDatabaseAPIAuth"
    //            },
    //        },
    //        new List<string>()
    //    }
    //});
});

// custom services: inject interfaceX, provide an implementation of concrete type Y
builder.Services.AddDbContext<ElleChristineDbContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:elleChristineDbConnectionString"]));
builder.Services.AddScoped<IElleChristineDbRepository, ElleChristineDbRepository>();
builder.Services.AddScoped<IShowProcessor, ShowProcessor>();

// sets content type to return based on file extension of file.
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddHealthChecks();


// auto-mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//// authentication token services. bearer token authentication middleware
//builder.Services.AddAuthentication("Bearer").AddJwtBearer((options) =>
//{
//    // how we validate the token
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        // the expiration time of the token is automatically validated - nothing to do here.

//        // items we want to validate
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,

//        // who can issue the token
//        ValidIssuer = builder.Configuration["Authentication:Issuer"],

//        // who can use this token?
//        ValidAudience = builder.Configuration["Authentication:Audience"],

//        // this signing key matches the values we used for creating the token (in controller).
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
//    };
//});

//// policies
//builder.Services.AddAuthorization((options) =>
//{
//    // https://app.pluralsight.com/course-player?clipId=a2ec8948-6104-4cbd-9688-6ab6d312d90a
//    options.AddPolicy("EntriesRequireClaim", policy =>
//    {
//        policy.RequireAuthenticatedUser();

//        // requires the resources in the token provided to be "all"
//        policy.RequireClaim("resources", "entries");
//    });
//});






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
