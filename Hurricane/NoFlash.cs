using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane
{
    public class NoFlash : Player
    {

        public void SkipFlashVisible()
        {
            if (Game.Read(Player.GetValue() + Offsets.m_flFlashMaxAlpha) > 0)
                Game.WriteFloat(0.0f, Player.GetValue() + Offsets.m_flFlashMaxAlpha);
        }





    }
}
