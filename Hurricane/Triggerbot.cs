using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hurricane
{
    /// <summary>
    /// 3/4 of this creap is from my "old" triggerbot, I have no desire to fix this    
    /// </summary>
    public class Triggerbot
    {

        private int activeWeapon { get; set; }
        private int weaponEntity { get; set; }

        private int availableShells { get; set; }
        private int availableShellsNonEmpty { get; set; }
        private int actualWeaponID { get; set; }

        private int[] pistols = { 1, 2, 3, 4, 30, 32, 36, 61, 63 };
        private int[] shotguns = { 25, 27, 29, 35 };
        private int[] SMGs = { 17, 19, 24, 26, 33, 34 };
        private int[] rifles = { 7, 8, 10, 13, 16, 39, 40, 60 };
        private int[] noobWeapons = { 11, 14, 28, 38 };


        /// <summary>
        /// 
        /// </summary>
        public void WeaponAssistance()
        {          
        
            while (ThreadValues.WeaponSupport)
            {

                AmmoDetection();
                if (ThreadValues.Triggerbot)
                {                    
                    actualWeaponID = ActualHoldWeapon();
                    if (EnemyDetection())
                    {
                        Thread.Sleep(ThreadValues.ReactionTime);                        
                        if (AllowedWeapon(ActualHoldWeapon()))
                        {
                            if (actualWeaponID == 9 && ThreadValues.QuickScope)
                            {
                                ZoomCheck();
                                PullTheTrigger();
                                ChangeWeapon();
                            }

                            else if (actualWeaponID == 9 && !ThreadValues.QuickScope)
                            {

                                PullTheTrigger();
                                ChangeWeapon();
                            }
                            else
                            {
                                if (Game.GetAsyncKeyState(0x11) != 0 && ThreadValues.FireControl)
                                {                                    
                                    if (FireControl() <= 1013)
                                        PullTheTrigger();

                                }
                                else
                                {
                                    PullTheTrigger();
                                }
                            }

                        }
                    }
                }


                if (ThreadValues.WeaponChangeAtReload == true && Game.GetAsyncKeyState(0x52) != 0)
                {
                    AmmoDetectionNonEmpty();
                }

                Thread.Sleep(5);
            }        

    }



        /// <summary>
        /// We check to see if we can use current weapon
        /// </summary>
        /// <param name="actual"> current equipped weaponID as int </param>
        /// <returns> returning allow or disallow as bool </returns>
        private bool AllowedWeapon(int actual)
        {

            if (ThreadValues.AWP && actual == 9)
                return true;


            if (ThreadValues.Pistols)
            {
                foreach(int allowed in pistols)
                {
                    if (actual == allowed)
                        return true;
                }
            }   

            if(ThreadValues.Rifles)
            {
                foreach(int allowed in rifles)
                {
                    if (actual == allowed)
                        return true;
                }
            }

            if(ThreadValues.SMGs)
            {
                foreach(int allowed in SMGs)
                {
                    if (actual == allowed)
                        return true;
                }
            }

            if(ThreadValues.Shotguns)
            {
                foreach(int allowed in shotguns)
                {
                    if (actual == allowed)
                        return true;
                }
            }

            if(ThreadValues.NoobWeapons)
            {
                foreach(int allowed in noobWeapons)
                {
                    if (actual == allowed)
                        return true;
                }
            }
            return false;
        }
        

        /// <summary>
        /// inspection current weaponID
        /// </summary>
        /// <returns> weaponID as int</returns>
        private int ActualHoldWeapon()
        {
            activeWeapon = Game.Read(Player.GetValue() + Offsets.m_hActiveWeapon);
            activeWeapon &= 0xFFF;
            weaponEntity = Game.Read((Offsets.m_dwEntityList + AutoInjector.BaseAddressClientDLL + activeWeapon * 0x10) - 0x10);
            return Game.Read(weaponEntity + Offsets.m_iItemDefinitionIndex);
        }


        /// <summary>
        /// Few guns can use zoom, if we have checked "quick scope" we need to check that, if is not zoomed in, we make it
        /// </summary>
        private void ZoomCheck()
        {
            int WeaponHandle = Game.Read(Player.GetValue() + Offsets.m_hActiveWeapon);
            WeaponHandle &= 0xFFF;
            weaponEntity = Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwEntityList + (WeaponHandle - 1) * 0x10);
            
            if (Game.Read(weaponEntity + Offsets.m_zoomLevel) == 0)
            {
                Game.mouse_event(0x008, 0, 0, 0, (System.IntPtr)0);
                Thread.Sleep(10);
                Game.mouse_event(0x010, 0, 0, 0, (System.IntPtr)0);
                Thread.Sleep(300);
            }
        }

        /// <summary>
        /// Just shot
        /// </summary>
        private void PullTheTrigger()
        {
            Game.mouse_event(0x002, 0, 0, 0, (System.IntPtr)0);
            Thread.Sleep(9);
            Game.mouse_event(0x004, 0, 0, 0, (System.IntPtr)0);
            Thread.Sleep(9);
        }

        /// <summary>
        /// Swap weapon
        /// </summary>
        private void ChangeWeapon()
        {
            Game.keybd_event(0x51, 0x10, 0, 0);
            Thread.Sleep(20);
            Game.keybd_event(0x51, 0x10, Game.KEYEVENTF_KEYUP, 0);
            Thread.Sleep(200);
            Game.keybd_event(0x51, 0x10, 0, 0);
            Thread.Sleep(20);
            Game.keybd_event(0x51, 0x10, Game.KEYEVENTF_KEYUP, 0);
            Thread.Sleep(1300);
        }

        /// <summary>
        /// Checking object under crosshair
        /// </summary>
        /// <returns> if under crosshair is enemy, return = true </returns>
        private bool EnemyDetection()
        {

            int Target = Game.Read(Player.GetValue() + Offsets.m_iCrossHairID);
            if (Target > 0 && Target <= 64)
            {
                int TargetAddress = Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwEntityList + ((Target - 1) * 0x10));
                int MyTeam = Game.Read(Player.GetValue() + Offsets.m_iTeamNum);
                int EnemyTeam = Game.Read(TargetAddress + Offsets.m_iTeamNum);
                int HPValue = Game.Read(TargetAddress + Offsets.m_iHealth);
                if (MyTeam != EnemyTeam && HPValue > 0)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Magic
        /// </summary>
        private void AmmoDetection()
        {
            AmmoCheck();
            if (availableShells == 0)
            {

                while (ThreadValues.WeaponChangeAtReload == true && availableShells == 0 )
                {
                    actualWeaponID = ActualHoldWeapon();
                    AmmoCheck();
                    if (actualWeaponID == 25 || actualWeaponID == 27 || actualWeaponID == 29 || actualWeaponID == 35)
                    {
                        break;
                    }

                    if (availableShells > 0)
                        ChangeWeapon();

                    else
                        Thread.Sleep(75);
                }


                while (ThreadValues.WeaponChangeAtReload == false && availableShells == 0 )
                {
                    AmmoCheck();
                    Thread.Sleep(75);
                }

            }
        }
        /// <summary>
        /// checking available bullets
        /// </summary>
        private void AmmoCheck()
        {                        
            int WeaponEntity = Game.Read((Offsets.m_dwEntityList + AutoInjector.BaseAddressClientDLL + activeWeapon * 0x10) - 0x10);
            availableShells = Game.Read(WeaponEntity + Offsets.m_iClip);
        }

        /// <summary>
        /// second magic
        /// </summary>
        /// <returns> penalty as int </returns>
        private int FireControl()
        {

            int Player = Game.Read(AutoInjector.BaseAddressClientDLL + Offsets.m_dwLocalPlayer);
            int ActiveWeapon = Game.Read(Player + Offsets.m_hActiveWeapon);
            ActiveWeapon = ActiveWeapon & 0xFFF;
            int WeaponEntity = Game.Read((Offsets.m_dwEntityList + AutoInjector.BaseAddressClientDLL + ActiveWeapon * 0x10) - 0x10);
            float Penalty = Game.Read(WeaponEntity + Offsets.m_fAccuracyPenalty);
            int ControlIndex = Convert.ToInt32(Penalty);
            ControlIndex = ControlIndex / 1000000;
            return ControlIndex;
        }

        /// <summary>
        /// third magic
        /// </summary>
        private void AmmoDetectionNonEmpty()
        {
            AmmoCheck();
            availableShellsNonEmpty = availableShells;
            while (availableShells == availableShellsNonEmpty)
            {


                int ActiveWeapon = Game.Read(Player.GetValue() + Offsets.m_hActiveWeapon);
                ActiveWeapon = ActiveWeapon & 0xFFF;
                int WeaponEntity = Game.Read((Offsets.m_dwEntityList + AutoInjector.BaseAddressClientDLL + ActiveWeapon * 0x10) - 0x10);
                availableShellsNonEmpty = Game.Read(WeaponEntity + Offsets.m_iClip);

                actualWeaponID = ActualHoldWeapon();
                if (actualWeaponID == 25 || actualWeaponID == 27 || actualWeaponID == 29 || actualWeaponID == 35)
                {
                    break;
                }

                if (availableShells < availableShellsNonEmpty)
                    ChangeWeapon();

                else
                    Thread.Sleep(75);
            }
        }


       
    }
}

