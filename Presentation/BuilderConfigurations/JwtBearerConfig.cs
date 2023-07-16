using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation.BuilderConfigurations
{
    public class JwtBearerConfig
    {

        public static Action<JwtBearerOptions> Configuration = options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(BuilderConfiguration.BuilderConfig.GetSection("AppSettings:Token").Value!)
                    )
            };
        };
    }
}
