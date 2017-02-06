using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane
{
    /// <summary>
    /// Shortcut for getting values which repeat in code
    /// </summary>
    public class Player
    {
        public static int GetValue()
        {
            return Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwLocalPlayer);
        }

        public static int MyTeam()
        {
            return Game.Read(GetValue() + Offsets.m_iTeamNum);
        }

        public static int GlowPointer()
        {
            return Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwGlowObject);
        }
    }
}
