using Application.Interfaces.Repository;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceExtentions
    {
        public static void InfrastructureServiceConfiguration(this IServiceCollection services) 
        {

            // DI for context
            services.AddDbContext<ApplicationDbContext>();

            // DI for Repositories
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITicketsRepository, TicketsRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IFAQCategoriesRepository, FAQCategoriesRepository>();
            services.AddScoped<IFAQItemsRepository, FAQItemsRepository>();
            services.AddScoped<IMessageAttachmentsRepository, MessageAttachmentsRepository>();

        }
    }
}
