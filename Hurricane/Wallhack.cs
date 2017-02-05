using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hurricane
{
    public class Wallhack : Player
    {
        ColorValues Enemy = new ColorValues(1.0f);
        ColorValues Friend = new ColorValues(1.0f);


        struct ColorValues
        {

            public float Red;
            public float Green;
            public float Blue;
            public float Alpha;

            public ColorValues(float alpha)
            {
                Alpha = alpha;
                Red = 0.0f;
                Green = 0.0f;
                Blue = 0.0f;
            }
        }

        

        private void CheckColor()
        {
            
            Enemy.Red = (float)ThreadValues.EnemyRed / 255;
            Enemy.Green = (float)ThreadValues.EnemyGreen / 255;
            Enemy.Blue = (float)ThreadValues.EnemyBlue / 255;

            Friend.Red = (float)ThreadValues.FriendRed / 255;
            Friend.Green = (float)ThreadValues.FriendGreen / 255;
            Friend.Blue = (float)ThreadValues.FriendBlue / 255;
        }

        public void GlowPlayers()
        {
            CheckColor();
            int glowPointer = Player.GlowPointer();
            int myTeam = Player.MyTeam();

            for (int i = 0; i < 32; i++)
            {
                int currentPlayer = Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwEntityList + i * 0x10);
                int currentPlayerDormant = Game.Read(currentPlayer + Offsets.m_bDormant);
                int currentPlayerGlowIndex = Game.Read(currentPlayer + Offsets.m_iGlowIndex);
                int currentPlayerTeam = Game.Read(currentPlayer + Offsets.m_iTeamNum);

                if (currentPlayerDormant == 1 || currentPlayerTeam == 0)
                    continue;

                else
                {
                    if (myTeam != currentPlayerTeam)
                    {
                        Game.WriteFloat(Enemy.Red, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x4);
                        Game.WriteFloat(Enemy.Green, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x8);
                        Game.WriteFloat(Enemy.Blue, glowPointer + (currentPlayerGlowIndex * 0x38) + 0xC);
                        Game.WriteFloat(Enemy.Alpha, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x10);
                        Game.WriteBool(true, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x24);
                        Game.WriteBool(false, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x25);
                    }

                    if (myTeam == currentPlayerTeam && ThreadValues.FriendlyWallhack)
                    {
                        Game.WriteFloat(Friend.Red, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x4);
                        Game.WriteFloat(Friend.Green, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x8);
                        Game.WriteFloat(Friend.Blue, glowPointer + (currentPlayerGlowIndex * 0x38) + 0xC);
                        Game.WriteFloat(Friend.Alpha, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x10);
                        Game.WriteBool(true, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x24);
                        Game.WriteBool(false, glowPointer + (currentPlayerGlowIndex * 0x38) + 0x25);
                    }
                }

            }
        }   


       

    }
}



