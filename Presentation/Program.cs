namespace Presentation
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {

            var app = Builder.BuildApp(args);

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