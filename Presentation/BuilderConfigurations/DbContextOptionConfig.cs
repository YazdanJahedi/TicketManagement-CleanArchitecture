using Microsoft.EntityFrameworkCore;

namespace Presentation.BuilderConfigurations
{
    public static class DbContextOptionConfig
    {
        public static Action<DbContextOptionsBuilder> Configuration = opt =>
        {
            opt.UseSqlServer(BuilderConfiguration.BuilderConfig.GetConnectionString("DateBase"));
        };
    }
}
