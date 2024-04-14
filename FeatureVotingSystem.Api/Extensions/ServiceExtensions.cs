using FeatureVotingSystem.Api.Auth;
using FeatureVotingSystem.Core.Products.Features.CreateProduct;
using FeatureVotingSystem.Core.Products.Features.DeleteProduct;
using FeatureVotingSystem.Core.Products.Features.UpdateProduct;
using
    FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesByVotesQuantityAndStatus;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesQuantity;
using FeatureVotingSystem.Core.Reports.Features.ProductAuthorReportRequests.GetRequestedFeaturesVotesQuantity;
using FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetUserRequestedFeaturesVotesQuantity;
using FeatureVotingSystem.Core.Services.Logger;
using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using FeatureVotingSystem.Core.Comments.Features.AddComment;
using FeatureVotingSystem.Core.ProductFeatures.Features.AddVote;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureProperties;
using FeatureVotingSystem.Core.ProductFeatures.Features.ChangeFeatureStatus;
using FeatureVotingSystem.Core.ProductFeatures.Features.CreateFeature;
using FeatureVotingSystem.Core.ProductFeatures.Features.DeleteFeature;
using FeatureVotingSystem.Core.Reports.Features.UserReportRequests.GetRequestedFeaturesByStatus;
using FeatureVotingSystem.Core.Shared.Features.GetFeature;
using FeatureVotingSystem.Core.Shared.Features.GetUser;
using FeatureVotingSystem.Core.Users;
using FeatureVotingSystem.Core.Users.Features.LoginUser;
using FeatureVotingSystem.Core.Users.Features.RegisterUser;
using FeatureVotingSystem.Shared;
using FeatureVotingSystem.Shared.Features.AddEmailInQueue;
using FeatureVotingSystem.Shared.Features.GetQueuedEmails;
using FeatureVotingSystem.Shared.Features.UpdateEmailQueueStatus;
using FeatureVotingSystem.Worker.EmailSenderService;
using Microsoft.EntityFrameworkCore;

namespace FeatureVotingSystem.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

    public static void ConfigureAuthenticationAndJwtBearer(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    JwtConfigurationHelper.GetTokenValidationParameters(configuration["Jwt:Key"]!);
            });

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<AuthDbContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString(Environment.UserName)));

    public static void AddAuthorizationPolicy(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy("MyApiUserPolicy",
                policy => policy.RequireClaim(ClaimTypes.Role, "api-user"));
            options.AddPolicy("AdminPolicy",
                policy => policy.RequireClaim(ClaimTypes.Role, "api-admin"));
        });

    public static void AddIdentityConfiguration(this IServiceCollection services) =>
        services.AddIdentity<UserEntity, IdentityRole<int>>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetClaimAuthorization", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

    public static void AddDataAccessAndBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<DapperContext>();

        services.AddSingleton<JwtTokenGenerator>();

        services.AddScoped<ICreateProductRepository, CreateProductRepository>();
        services.AddScoped<ICreateProductService, CreateProductService>();
        services.AddScoped<IUpdateProductRepository, UpdateProductRepository>();
        services.AddScoped<IUpdateProductService, UpdateProductService>();
        services.AddScoped<IDeleteProductRepository, DeleteProductRepository>();
        services.AddScoped<IDeleteProductService, DeleteProductService>();
        services.AddScoped<IGetProductRepository, GetProductRepository>();

        services.AddScoped<IAddCommentRepository, AddCommentRepository>();
        services.AddScoped<IAddCommentService, AddCommentService>();

        services.AddScoped<IGetFeatureRepository, GetFeatureRepository>();
        services.AddScoped<ICreateFeatureRepository, CreateFeatureRepository>();
        services.AddScoped<ICreateFeatureService, CreateFeatureService>();
        services.AddScoped<IDeleteFeatureRepository, DeleteFeatureRepository>();
        services.AddScoped<IDeleteFeatureService, DeleteFeatureService>();
        services.AddScoped<IChangeFeaturePropertiesRepository, ChangeFeaturePropertiesRepository>();
        services.AddScoped<IChangeFeaturePropertiesService, ChangeFeaturePropertiesService>();
        services.AddScoped<IChangeFeatureStatusRepository, ChangeFeatureStatusRepository>();
        services.AddScoped<IChangeFeatureStatusService, ChangeFeatureStatusService>();
        services.AddScoped<IVoteRepository, VoteRepository>();
        services.AddScoped<IAddVoteService, AddVoteService>();

        services.AddScoped<IGetUserByIdRepository, GetUserByIdByIdRepository>();
        services.AddScoped<ILoginUserService, LoginUserService>();
        services.AddScoped<ICheckUserPasswordRepository, CheckUserPasswordRepository>();
        services.AddScoped<IAddRoleToUserRepository, AddRoleToUserRepository>();
        services.AddScoped<IRegisterUserService, RegisterUserService>();
        services.AddScoped<IRegisterUserRepository, RegisterUserRepository>();
        services.AddScoped<IGetUserByEmailRepository, GetUserByEmailRepository>();

        services.AddSingleton<IEmailSender, EmailSender>();

        services.AddScoped<IGetRequestedFeaturesQuantityRepository, GetRequestedFeaturesQuantityRepository>();
        services.AddScoped<IGetRequestedFeaturesQuantityService, GetRequestedFeaturesQuantityService>();
        services.AddScoped<IGetRequestedFeaturesVotesQuantityRepository, GetRequestedFeaturesVotesQuantityRepository>();
        services.AddScoped<IGetRequestedFeaturesVotesQuantityService, GetRequestedFeaturesVotesQuantityService>();
        services
            .AddScoped<IGetRequestedFeaturesByVotesQuantityAndStatusRepository,
                GetRequestedFeaturesByVotesQuantityAndStatusRepository>();
        services
            .AddScoped<IGetRequestedFeaturesByVotesQuantityAndStatusService,
                GetRequestedFeaturesByVotesQuantityAndStatusService>();
        services.AddScoped<IGetRequestedFeaturesByStatusRepository, GetRequestedFeaturesByStatusRepository>();
        services.AddScoped<IGetRequestedFeaturesByStatusService, GetRequestedFeaturesByStatusService>();
        services
            .AddScoped<IGetUserRequestedFeaturesVotesQuantityRepository,
                GetUserRequestedFeaturesVotesQuantityRepository>();
        services
            .AddScoped<IGetUserRequestedFeaturesVotesQuantityService, GetUserRequestedFeaturesVotesQuantityService>();

        services.AddScoped<IAddEmailInQueueRepository, AddEmailInQueueRepository>();
        services.AddScoped<IGetQueuedEmailsRepository, GetQueuedEmailsRepository>();
        services.AddScoped<IUpdateEmailQueueStatusRepository, UpdateEmailQueueStatusRepository>();
    }
}