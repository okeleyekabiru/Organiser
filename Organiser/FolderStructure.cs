using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Organiser
{
    public class FolderStructure : IHostedService, IDisposable
    {
        private readonly ILogger<FolderStructure> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;

        public FolderStructure(ILogger<FolderStructure> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)

        {
         
                
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganiserContext>();
                context.Database.EnsureCreated();


            }
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(RunTask, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }
    
        private async void RunTask(object state)
        {
            try
            {

                Settings Settings = null;
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<OrganiserContext>();
                    context.Database.EnsureCreated();

                    var scheduler = scope.ServiceProvider.GetRequiredService<IScheduler>();
                    var storedSettings = await scheduler.Get();
                    if (storedSettings != null)
                    {
                        Settings = storedSettings.Data.DeserializeSettings<Settings>();


                        if (Settings.NextRun.Value == DateTime.Now ||Settings.NextRun.Value < DateTime.Now)
                        {
                      
                           Utilities.EnumerateAndMoveCategorize();
                            await scheduler.Remove(storedSettings);
                            var settings = new Settings
                            {
                                NextRun = DateTime.Now.AddHours(1),
                                LastRun = DateTime.Now
                            };


                            await scheduler.Add(settings.SerializeSettings());
                            await context.SaveChangesAsync();


                        }

                    

                    }
                    else
                    {
                        
                           Utilities.EnumerateAndMoveCategorize();
                            
                            var settings = new Settings
                            {
                                NextRun = DateTime.Now.AddHours(1),
                                LastRun = DateTime.Now
                            };


                            await scheduler.Add(settings.SerializeSettings());
                            await context.SaveChangesAsync();

                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occured while checking files");

            }

        }
  

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Organiser as been stopped");

            _timer?.Change(Timeout.Infinite, 0);


            return Task.CompletedTask;
        }
       

        
       
       
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
