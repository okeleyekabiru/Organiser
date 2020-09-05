using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Organiser.Utilities;

namespace Organiser
{
    public class FolderStructure : IHostedService,IDisposable
    {
        private readonly ILogger<FolderStructure> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;

        public FolderStructure(ILogger<FolderStructure> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
               var context = scope.ServiceProvider.GetRequiredService<OrganiserContext>();
                context.Database.EnsureCreated();


            }
                _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(RunTask, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

           
        }

        private async void RunTask(object state)
        {

            _logger.LogInformation("checking Database");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganiserContext>();
                context.Database.EnsureCreated();

                var scheduler = scope.ServiceProvider.GetRequiredService<IScheduler>();
                var storedSettings = await scheduler.Get();
                
                if (storedSettings != null && storedSettings.Data.DeserializeSettings<Settings>().NextRun.Value < DateTime.Now )
                {
                    await scheduler.Remove(storedSettings);
                    var settings = new Settings
                    {
                        NextRun = DateTime.Now.AddHours(24),
                        LastRun = DateTime.Now
                    };


                    await scheduler.Add(settings.SerializeSettings());
                    await context.SaveChangesAsync();


                }

               

            }
           
            
            _logger.LogInformation(
                "Checking Download folder for files");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Organiser as been stopped");

            _timer?.Change(Timeout.Infinite, 0);


            return Task.CompletedTask;
        }
        private void EnumerateAndMoveCategorize()
        {
            var downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            foreach (var item in Directory.GetFiles(downloadPath))
            {
                Console.WriteLine(item);

            }
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
