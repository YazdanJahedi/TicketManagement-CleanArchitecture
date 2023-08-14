
using Presentation.Installer;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args).RegisterServices();

            var app = builder.Build().SetupMiddlewares();

            app.Run();
        }
    }
}