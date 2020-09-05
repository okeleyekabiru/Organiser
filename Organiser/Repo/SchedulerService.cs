using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Organiser
{
    internal class SchedulerService : IScheduler
    {
        private readonly OrganiserContext context;

        public SchedulerService(OrganiserContext context)
        {
            this.context = context;
        }
        public async Task Add (string data)
        {
            var scheduler = new Scheduler { Data =data, Id = Guid.NewGuid() };

            await context.AddAsync(scheduler);
        }

        public async Task<bool> Commit()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Scheduler> Get()
        {
            return   await context.Schedulers.FirstOrDefaultAsync();
           
        }

        public  Task Remove(Scheduler data)
        {
            context.Remove(data);
            return Task.CompletedTask;
        }
    }
}
