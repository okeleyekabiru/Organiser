using Organiser.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organiser
{
    interface IScheduler
    {
        Task Add(string data);
        Task Remove( Scheduler data);
        Task<Scheduler> Get();
        Task<bool> Commit();
    }
}
