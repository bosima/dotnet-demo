using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add OpenTelemetry Log Exporter
builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
            {
                options.AddConsoleExporter();
            });

// Add OpenTelemetry Tracing Exporter
builder.Services.AddOpenTelemetryTracing((builder) => builder
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("bossma-jeager-test"))
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddJaegerExporter());

builder.Services.Configure<JaegerExporterOptions>(options=>{
    options.AgentHost="localhost";
    options.AgentPort=6831;

});

// Customize the HttpClient that will be used when JaegerExporter is configured for HTTP transport.
// builder.Services.AddHttpClient("JaegerExporter", configureClient: (client) => client.DefaultRequestHeaders.Add("X-MyCustomHeader", "value"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
