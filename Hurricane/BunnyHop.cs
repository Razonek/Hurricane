using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Windows.Threading;

namespace Hurricane
{

    /// <summary>
    /// Class responsible for auto jumping when player touch ground while pressing bind key
    /// </summary>
    public static class BunnyHop
    {

         /// <summary>
         /// Turning on bunnyhop
         /// </summary>
        public static void GetJump()
        {
                           
            while (ThreadValues.BunnyHop)
            {
                if (Game.GetAsyncKeyState(MainWindow.BunnyhopBindKey) != 0)
                {
                    if (GetFlag() == 257)
                    {
                        Game.keybd_event(0x20, 0x39, 0, 0);
                        Thread.Sleep(20);
                        Game.keybd_event(0x20, 0x39, Game.KEYEVENTF_KEYUP, 0);
                    }
                }
                else
                    Thread.Sleep(50);                
            }            
        }



        /// <summary>
        /// Getting player statement, 257 = ground, 256 = air
        /// </summary>
        private static int GetFlag()
        {
            

            int Player = Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwLocalPlayer);
            return Game.Read(Player + Offsets.m_fFlags);
        }
    }
}
