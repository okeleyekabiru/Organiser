using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Organiser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel.Override("Microsoft", LogEventLevel.Warning).Enrich.FromLogContext().WriteTo.File(@$"c:\Organiser\log_{Guid.NewGuid()}.txt").CreateLogger();
            try
            {
                Log.Information("Organiser starting......");
                if (args.Length == 0)
                {
                    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                }
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception ex)
            {

                Log.Fatal(ex, "an error occurd while starting organiser");
                    return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<FolderStructure>();
                    services.AddDbContext<OrganiserContext>(opt =>
                    {
                        opt.UseSqlite("Data Source=./mydb.db;");
                    });
                    services.AddScoped<IScheduler, SchedulerService>();
                }).UseSerilog();
    }
}
