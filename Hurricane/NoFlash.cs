using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane
{
    /// <summary>
    /// No flash implementation, simple and clean
    /// </summary>
    public class NoFlash : Player
    {
        /// <summary>
        /// Override game flash light
        /// </summary>
        public void SkipFlashVisible()
        {
            if (Game.Read(Player.GetValue() + Offsets.m_flFlashMaxAlpha) > 0)
                Game.WriteFloat(0.0f, Player.GetValue() + Offsets.m_flFlashMaxAlpha);
        }





    }
}
