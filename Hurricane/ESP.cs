using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hurricane
{
    /// <summary>
    /// Everything what we can see in game
    /// </summary>
    public class ESP
    {
        NoFlash NoFlashUtility;
        Wallhack WallhackUtility;

        /// <summary>
        /// Creating new instance of each class
        /// </summary>
        public ESP()
        {
            NoFlashUtility = new NoFlash();
            WallhackUtility = new Wallhack();
        }


        /// <summary>
        /// Turning on/off effects on toggle buttons
        /// </summary>
        public void VisualFX()
        {
            while(ThreadValues.ESP)
            {
                if (ThreadValues.NoFlash)
                {
                    NoFlashUtility.SkipFlashVisible();
                }

                if (ThreadValues.Wallhack)
                {
                    WallhackUtility.GlowPlayers();
                }

                if (!ThreadValues.NoFlash && !ThreadValues.Wallhack)
                    Thread.Sleep(50);
                Thread.Sleep(1);
            }            
        }



    }
}
