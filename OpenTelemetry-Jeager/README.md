**1. Start Jaeger**

    docker run -d --name jaeger -e COLLECTOR_ZIPKIN_HOST_PORT=:9411 -e COLLECTOR_OTLP_ENABLED=true  -p 6831:6831/udp -p 6832:6832/udp -p 5778:5778 -p 16686:16686 -p 4317:4317  -p 4318:4318 -p 14250:14250  -p 14268:14268  -p 14269:14269  -p 9411:9411  jaegertracing/all-in-one:1.35

**2. Config Service**

Add the packages:

    <ItemGroup>
        <PackageReference Include="OpenTelemetry" Version="1.3.0" />
        <PackageReference Include="OpenTelemetry.Exporter.Console" Version="1.3.0" />
        <PackageReference Include="OpenTelemetry.Exporter.Jaeger" Version="1.3.0" />
        <PackageReference Include="OpenTelemetry.Extensions.Hosting" Version="1.0.0-rc9.4" />
        <PackageReference Include="OpenTelemetry.Instrumentation.AspNetCore" Version="1.0.0-rc9.4" />
        <PackageReference Include="OpenTelemetry.Instrumentation.Http" Version="1.0.0-rc9.4" />
        <PackageReference Include="OpenTelemetry.Instrumentation.SqlClient" Version="1.0.0-rc9.4" />
    </ItemGroup>

Register OpenTelemetry Tracing Service:

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

**3. Run Service**

    dotnet run

**4. Test**

Visit the url:
> http://localhost:{Port}/WeatherForecast

Open the jeager ui: http://localhost:16686/