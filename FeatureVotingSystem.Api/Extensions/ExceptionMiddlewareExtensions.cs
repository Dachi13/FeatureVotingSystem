using FeatureVotingSystem.Core.Services.Logger;
using FeatureVotingSystem.Shared.Entities.ErrorModel;
using FeatureVotingSystem.Shared.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Net;

namespace FeatureVotingSystem.Api.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    string userFriendlyErrorMessageForSqlExceptions = "";

                    if(contextFeature.Error is SqlException) userFriendlyErrorMessageForSqlExceptions = "Internal server error occured";
                    else
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            BadRequestException => StatusCodes.Status400BadRequest,
                            NotFoundException => StatusCodes.Status404NotFound,
                            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        userFriendlyErrorMessageForSqlExceptions = contextFeature.Error.Message;
                    }

                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = userFriendlyErrorMessageForSqlExceptions,
                    }.ToString());
                }
            });
        });
    }
}