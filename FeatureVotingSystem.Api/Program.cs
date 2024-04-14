using FeatureVotingSystem.Api.Extensions;
using FeatureVotingSystem.Core.Services.Logger;
using FeatureVotingSystem.Worker;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.Services.AddSerilog(logger);
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthenticationAndJwtBearer(builder.Configuration);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAuthorizationPolicy();
builder.Services.AddIdentityConfiguration();
builder.Services.AddDataAccessAndBusinessLogicServices();
builder.Services.ConfigureLoggerService();
builder.Services.AddMemoryCache();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<Config>(builder.Configuration);
builder.Services.AddHostedService<EmailWorker>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

var loggerManager = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(loggerManager);

app.UseSerilogRequestLogging();

if(app.Environment.IsProduction())
    app.UseHsts();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Feature Voting System API"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();

app.Run();
