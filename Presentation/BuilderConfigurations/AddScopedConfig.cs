using Application.Repository;
using Infrastructure.Repository;

namespace Presentation.BuilderConfigurations
{
    public static class AddScopedConfig
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ITicketsRepository, TicketsRepository>();
            builder.Services.AddScoped<IResponsesRepository, ResponsesRepository>();
            builder.Services.AddScoped<IFAQCategoriesRepository, FAQCategoriesRepository>();
            builder.Services.AddScoped<IFAQItemsRepository, FAQItemsRepository>();
        }
    }
}
