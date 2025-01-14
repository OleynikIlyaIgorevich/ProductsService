var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var kestrelSection = configuration.GetSection("Kestrel");
    serverOptions.Configure(kestrelSection);
}).UseKestrel();

builder.Services.AddDatabase(configuration);
builder.Services.AddUnitOfWork();
builder.Services.AddRepositories();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(
    options => {
        options.SuppressAsyncSuffixInActionNames = false;
    }
)
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins();
        });
});

builder.Services.AddSwagger();


var app = builder.Build();

app.AddSwagger();

app.UseCors();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();
