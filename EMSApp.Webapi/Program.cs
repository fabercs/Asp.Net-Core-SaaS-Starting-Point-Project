using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EMSApp.Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(new string[]
                    {
                        "http://localhost:5001",
                        "http://localhost:5002",
                        "http://localhost:5003"
                    });
                });
    }
}
