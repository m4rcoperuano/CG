using CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeGeneration.Services
{
    public class SystemClockService : ISystemClock
    {
        public DateTime RightNow()
        {
            return DateTime.Now;
        }
    }
}