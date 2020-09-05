using System;
using System.Collections.Generic;
using System.Text;

namespace Organiser
{
   public class Settings
    {
      
        public DateTime? NextRun { get; set; }
        public DateTime? LastRun { get; set; }
        public TimeSpan  Interval { get; set; }
    }
}
