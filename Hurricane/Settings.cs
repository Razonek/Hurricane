using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hurricane
{
    /// <summary>
    /// Our settings will be saved...
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Saving all aplication settings like slider values, bind keys, etc.
        /// </summary>
        public static void SaveSettings()
        {
            // Bind keys 
            Properties.Settings.Default.TriggerbotToggleBindName = ((MainWindow)Application.Current.MainWindow).TriggerbotToggleBindName;
            Properties.Settings.Default.TriggerbotToggleBindKey = MainWindow.TriggerbotToggleBindKey;
            Properties.Settings.Default.WallhackToggleBindName = ((MainWindow)Application.Current.MainWindow).WallhackToggleBindName;
            Properties.Settings.Default.WallhackToggleBindKey = MainWindow.WallhackToggleBindKey;
            Properties.Settings.Default.NoFlashToggleBindName = ((MainWindow)Application.Current.MainWindow).NoFlashToggleBindName;
            Properties.Settings.Default.NoFlashToggleBindKey = MainWindow.NoFlashToggleBindKey;
            Properties.Settings.Default.BunnyhopBindName = ((MainWindow)Application.Current.MainWindow).BunnyhopBindName;
            Properties.Settings.Default.BunnyhopBindKey = MainWindow.BunnyhopBindKey;

            // Wallhack Settings
            Properties.Settings.Default.RedEnemy = ((MainWindow)Application.Current.MainWindow).RedEnemy;
            Properties.Settings.Default.GreenEnemy = ((MainWindow)Application.Current.MainWindow).GreenEnemy;
            Properties.Settings.Default.BlueEnemy = ((MainWindow)Application.Current.MainWindow).BlueEnemy;            
            Properties.Settings.Default.RedFriend = ((MainWindow)Application.Current.MainWindow).RedFriend;
            Properties.Settings.Default.GreenFriend = ((MainWindow)Application.Current.MainWindow).GreenFriend;
            Properties.Settings.Default.BlueFriend = ((MainWindow)Application.Current.MainWindow).BlueFriend;
            Properties.Settings.Default.FriendlyWallhack = ((MainWindow)Application.Current.MainWindow).FriendlyWallhack;

            // Allowed Weapons in triggerbot settings
            Properties.Settings.Default.AllowedAWP = ((MainWindow)Application.Current.MainWindow).AllowedAWP;
            Properties.Settings.Default.AllowedPistols = ((MainWindow)Application.Current.MainWindow).AllowedPistols;
            Properties.Settings.Default.AllowedShotguns = ((MainWindow)Application.Current.MainWindow).AllowedShotguns;
            Properties.Settings.Default.AllowedRifles = ((MainWindow)Application.Current.MainWindow).AllowedRifles;
            Properties.Settings.Default.AllowedSMGs = ((MainWindow)Application.Current.MainWindow).AllowedSMGs;
            Properties.Settings.Default.AllowedNoobWeapons = ((MainWindow)Application.Current.MainWindow).AllowedNoobsWeapons;

            // Allowed Other Settings
            Properties.Settings.Default.AllowedQuickScope = ((MainWindow)Application.Current.MainWindow).AllowedQuickScope;
            Properties.Settings.Default.AllowedAimImprove = ((MainWindow)Application.Current.MainWindow).AllowedAimImprove;
            Properties.Settings.Default.AllowedSwapWeapon = ((MainWindow)Application.Current.MainWindow).AllowedSwapWeapon;

            // Triggerbot reaction time
            Properties.Settings.Default.ReactionTime = ((MainWindow)Application.Current.MainWindow).ReactionTime;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Loading at startup our previous settings
        /// </summary>
        public static void LoadSettings()
        {
            // Setting bind keys
            MainWindow.TriggerbotToggleBindKey = Properties.Settings.Default.TriggerbotToggleBindKey;
            ((MainWindow)Application.Current.MainWindow).TriggerbotToggleBindName = Properties.Settings.Default.TriggerbotToggleBindName;
            MainWindow.WallhackToggleBindKey = Properties.Settings.Default.WallhackToggleBindKey;
            ((MainWindow)Application.Current.MainWindow).WallhackToggleBindName = Properties.Settings.Default.WallhackToggleBindName;
            MainWindow.NoFlashToggleBindKey = Properties.Settings.Default.NoFlashToggleBindKey;
            ((MainWindow)Application.Current.MainWindow).NoFlashToggleBindName = Properties.Settings.Default.NoFlashToggleBindName;
            MainWindow.BunnyhopBindKey = Properties.Settings.Default.BunnyhopBindKey;
            ((MainWindow)Application.Current.MainWindow).BunnyhopBindName = Properties.Settings.Default.BunnyhopBindName;

            // Setting Wallhack sliders + friendly wh -> update variables on value change event
            ((MainWindow)Application.Current.MainWindow).RedEnemySlider.Value = Properties.Settings.Default.RedEnemy;
            ((MainWindow)Application.Current.MainWindow).GreenEnemySlider.Value = Properties.Settings.Default.GreenEnemy;
            ((MainWindow)Application.Current.MainWindow).BlueEnemySlider.Value = Properties.Settings.Default.BlueEnemy;
            ((MainWindow)Application.Current.MainWindow).RedFriendSlider.Value = Properties.Settings.Default.RedFriend;
            ((MainWindow)Application.Current.MainWindow).GreenFriendSlider.Value = Properties.Settings.Default.GreenFriend;
            ((MainWindow)Application.Current.MainWindow).BlueFriendSlider.Value = Properties.Settings.Default.BlueFriend;
            ((MainWindow)Application.Current.MainWindow).FriendlyWallhackCheckbox.IsChecked = Properties.Settings.Default.FriendlyWallhack;
            ((MainWindow)Application.Current.MainWindow).FriendlyWallhack = Properties.Settings.Default.FriendlyWallhack;
            ThreadValues.FriendlyWallhack = Properties.Settings.Default.FriendlyWallhack;
          

            // Setting allowed weapons in triggerbot
            ((MainWindow)Application.Current.MainWindow).PistolsCheckbox.IsChecked = Properties.Settings.Default.AllowedPistols;
            ((MainWindow)Application.Current.MainWindow).AllowedPistols = Properties.Settings.Default.AllowedPistols;
            ((MainWindow)Application.Current.MainWindow).AWPCheckbox.IsChecked = Properties.Settings.Default.AllowedAWP;
            ((MainWindow)Application.Current.MainWindow).AllowedAWP = Properties.Settings.Default.AllowedAWP;
            ((MainWindow)Application.Current.MainWindow).ShotgunsCheckBox.IsChecked = Properties.Settings.Default.AllowedShotguns;
            ((MainWindow)Application.Current.MainWindow).AllowedShotguns = Properties.Settings.Default.AllowedShotguns;
            ((MainWindow)Application.Current.MainWindow).RiflesCheckbox.IsChecked = Properties.Settings.Default.AllowedRifles;
            ((MainWindow)Application.Current.MainWindow).AllowedRifles = Properties.Settings.Default.AllowedRifles;
            ((MainWindow)Application.Current.MainWindow).SMGsCheckbox.IsChecked = Properties.Settings.Default.AllowedSMGs;
            ((MainWindow)Application.Current.MainWindow).AllowedSMGs = Properties.Settings.Default.AllowedSMGs;
            ((MainWindow)Application.Current.MainWindow).NoobsCheckbox.IsChecked = Properties.Settings.Default.AllowedNoobWeapons;
            ((MainWindow)Application.Current.MainWindow).AllowedNoobsWeapons = Properties.Settings.Default.AllowedNoobWeapons;
            ThreadValues.AWP = Properties.Settings.Default.AllowedAWP;
            ThreadValues.Pistols = Properties.Settings.Default.AllowedPistols;
            ThreadValues.Rifles = Properties.Settings.Default.AllowedRifles;
            ThreadValues.Shotguns = Properties.Settings.Default.AllowedShotguns;
            ThreadValues.SMGs = Properties.Settings.Default.AllowedSMGs;
            ThreadValues.NoobWeapons = Properties.Settings.Default.AllowedNoobWeapons;
            

            // Setting "Other" 
            ((MainWindow)Application.Current.MainWindow).QuickScopeCheckbox.IsChecked = Properties.Settings.Default.AllowedQuickScope;
            ((MainWindow)Application.Current.MainWindow).AllowedQuickScope = Properties.Settings.Default.AllowedQuickScope;
            ((MainWindow)Application.Current.MainWindow).ReloadChangeCheckbox.IsChecked = Properties.Settings.Default.AllowedSwapWeapon;
            ((MainWindow)Application.Current.MainWindow).AllowedSwapWeapon = Properties.Settings.Default.AllowedSwapWeapon;
            ((MainWindow)Application.Current.MainWindow).AccuracyImproveCheckbox.IsChecked = Properties.Settings.Default.AllowedAimImprove;
            ((MainWindow)Application.Current.MainWindow).AllowedAimImprove = Properties.Settings.Default.AllowedAimImprove;
            ThreadValues.QuickScope = Properties.Settings.Default.AllowedQuickScope;
            ThreadValues.WeaponChangeAtReload = Properties.Settings.Default.AllowedSwapWeapon;
            ThreadValues.FireControl = Properties.Settings.Default.AllowedAimImprove;

            //Setting reaction time
            ((MainWindow)Application.Current.MainWindow).ReactionTimeSlider.Value = Properties.Settings.Default.ReactionTime;
            ThreadValues.ReactionTime = Properties.Settings.Default.ReactionTime;
        }
    }
}
