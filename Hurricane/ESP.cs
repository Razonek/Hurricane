using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hurricane
{
    public class ESP
    {
        NoFlash NoFlashUtility;
        Wallhack WallhackUtility;

        public ESP()
        {
            NoFlashUtility = new NoFlash();
            WallhackUtility = new Wallhack();
        }



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
            }            
        }



    }
}
