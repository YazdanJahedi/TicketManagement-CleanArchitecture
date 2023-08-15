using Application.Dtos.UserDtos;
using Application.Interfaces.Service;
using Application.Mappers.AuthMappers;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceExtentions
    {
        public static void ApplicationServiceConfiguration(this IServiceCollection services)
        {
            // Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(LoginRequest).Assembly);

            // DI for All Services
            services.AddScoped<IMessageAttachmentService, MessageAttachementtService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IAuthService, AuthService>();

            // HttpContextAccessor
            services.AddHttpContextAccessor();

            // AutoMapper
            services.AddAutoMapper(typeof(SignupMapper));
            services.AddControllersWithViews();
        }
    }
}
