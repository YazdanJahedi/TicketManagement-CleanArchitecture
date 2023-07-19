using Application.Repository;
using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Infrastructure.Repository;
using Presentation.BuilderConfigurations;

namespace Presentation
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            BuilderConfiguration.BuilderConfig = builder.Configuration;

            builder.Services.AddControllers().AddFluentValidation(FluentValidationConfig.Configuration);

            builder.Services.AddDbContext<ApplicationDbContext>(DbContextOptionConfig.Configuration);
            
            AddScopedConfig.Configure(builder);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(SwaggerGenConfig.Configuration);

            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerConfig.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}