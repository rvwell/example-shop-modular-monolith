
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services.AddOpenApi();

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app.Run();