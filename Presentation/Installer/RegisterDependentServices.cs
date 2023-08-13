using Application.Dtos.UserDtos;
using Application.Interfaces.Service;
using Application.Mappers;
using Application.Repository;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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

        private static void RegisterControllers(this IServiceCollection service)
        {
            service.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            service.AddFluentValidationAutoValidation();
            service.AddFluentValidationClientsideAdapters();
            service.AddValidatorsFromAssembly(typeof(LoginRequest).Assembly);

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
            service.AddScoped<IMessageAttachmentsRepository, MessageAttachmentsRepository>();

            service.AddScoped<IMessageAttachmentService, MessageAttachementtService>();
            service.AddScoped<ITicketService, TicketService>();


            service.AddHttpContextAccessor();

            // correct it
            service.AddMediatR(typeof(LoginHandler));

            //
            service.AddAutoMapper(typeof(SignupMapper));
            service.AddControllersWithViews();

        }

    }
}
