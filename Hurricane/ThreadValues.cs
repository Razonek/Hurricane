using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane
{
    /// <summary>
    /// Here are variables which access two threads at one time, no dispatchers or locks 
    /// </summary>
    public static class ThreadValues
    {
        public static bool BunnyHop { get; set; }
        public static bool Triggerbot { get; set; }
        public static bool NoFlash { get; set; }
        public static bool Wallhack { get; set; }
    }
}
