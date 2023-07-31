using Application.DTOs.UserDtos;
using Application.Features.UserFeatures.Commands.Signup;
using Application.Features.UserFeatures.Queries.Login;
using Application.Repository;
using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Infrastructure.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Presentation.Installer
{
    public static class RegisterDependentServices
    {
        [Obsolete]
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.RegisterControllers();
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.RegisterScoped();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.RegisterSwaggerGen();
            builder.Services.RegisterJwtBarear(builder.Configuration);

            return builder;
        }

        [Obsolete]
        private static void RegisterControllers(this IServiceCollection service)
        {
            service.AddControllers()
                .AddFluentValidation(options =>
                    {
                        options.ImplicitlyValidateChildProperties = true;
                        options.ImplicitlyValidateRootCollectionElements = true;
                        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    }
                ).AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

        }

        private static void RegisterSwaggerGen(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                });
        }

        private static void RegisterJwtBarear(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAuthentication()
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!)
                                )
                        };
                    });
        }

        private static void RegisterScoped(this IServiceCollection service)
        {
            service.AddScoped<IUsersRepository, UsersRepository>();
            service.AddScoped<ITicketsRepository, TicketsRepository>();
            service.AddScoped<IMessagesRepository, MessagesRepository>();
            service.AddScoped<IFAQCategoriesRepository, FAQCategoriesRepository>();
            service.AddScoped<IFAQItemsRepository, FAQItemsRepository>();
            service.AddHttpContextAccessor();

            // correct it
            service.AddMediatR(typeof(LoginRequestQuery));
            service.AddMediatR(typeof(SignupRequestQuery));
        }

    }
}
