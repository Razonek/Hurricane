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
        public static bool WeaponSupport { get; set; }
        public static bool Triggerbot { get; set; }
        public static bool NoFlash { get; set; }
        public static bool Wallhack { get; set; }
        public static bool ESP { get; set; }

        public static float EnemyRed { get; set; }
        public static float EnemyGreen { get; set; }
        public static float EnemyBlue { get; set; }

        public static float FriendRed { get; set; }
        public static float FriendGreen { get; set; }
        public static float FriendBlue { get; set; }

        public static bool FriendlyWallhack { get; set; }
        public static bool WeaponChangeAtReload { get; set; }
        public static bool QuickScope { get; set; }
        public static bool FireControl { get; set; }

        public static bool Pistols { get; set; }
        public static bool AWP { get; set; }
        public static bool Shotguns { get; set; }
        public static bool Rifles { get; set; }
        public static bool SMGs { get; set; }
        public static bool NoobWeapons { get; set; }

        public static int ReactionTime { get; set; }
    }
}
