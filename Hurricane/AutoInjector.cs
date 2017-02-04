using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;


namespace Hurricane
{
    /// <summary>
    /// Automatic injector
    /// </summary>
    public static class AutoInjector
    {
        public static bool Injected { get; set; }
        private static int pHandle { get; set; }
        public static int BaseAddressClientDLL { get; private set; }
        public static int GameHandle { get; private set; }



        /// <summary>
        /// Automatic inject and if game will be closed auto re-inject
        /// </summary>
        public static void Inject()
        {            
            while (MainWindow.InjectorThread)
            {
                if (!Injected)
                {
                    foreach (Process Processes in Process.GetProcesses())
                    {
                        if (Processes.ProcessName.Equals("csgo"))
                        {
                            pHandle = Game.OpenProcess(2035711, false, Processes.Id);
                            Thread.Sleep(5000);
                            foreach (ProcessModule Module in Processes.Modules)
                            {
                                if (Module.ModuleName.Equals("client.dll"))
                                {
                                    BaseAddressClientDLL = (int)Module.BaseAddress;
                                    GameHandle = pHandle;
                                    Injected = true;
                                }
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
                if (Injected)
                {
                    Process[] Proc = Process.GetProcessesByName("csgo");
                    if (Proc.Length == 0)
                    {
                        Injected = false;
                    }
                    Thread.Sleep(1000);
                }
                
            }

        }
    }
}
