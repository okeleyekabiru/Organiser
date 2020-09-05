using Microsoft.EntityFrameworkCore;

namespace Organiser
{
    internal class OrganiserContext:DbContext
    {
      
        public OrganiserContext(DbContextOptions<OrganiserContext> options):base(options)
        {
                
        }
      public  DbSet<Scheduler> Schedulers { get; set; }
    }
}