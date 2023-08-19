using Application.Interfaces.Repository;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;
using Application.Interfaces.Service;

namespace Infrastructure
{
    public static class ServiceExtentions
    {
        public static void InfrastructureServiceConfiguration(this IServiceCollection services) 
        {

            // DI for context
            services.AddDbContext<ApplicationDbContext>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // DI for All Services
            services.AddScoped<IMessageAttachmentService, MessageAttachementtService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
