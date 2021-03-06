﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace Hurricane
{
    /// <summary>
    /// Cheat for Counter Strike Global Offensive
    /// 
    /// Implemented solutions:
    /// -Triggerbot,
    /// -Simple Wallhack(glow),
    /// -No Flash,
    /// -Bunnyhop.
    /// 
    /// To implement:
    /// -Patter scanner.
    /// 
    /// Meybe:
    /// Directx Hook.
    /// 
    /// //////////////////////////////////////
    ///                                     //
    ///     C#, MySQL Developer: Razonek    //
    ///     PHP Developer: Rolowy           //
    ///                                     //
    /// //////////////////////////////////////
    /// </summary>    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        SolidColorBrush CustomBlue = new SolidColorBrush(Color.FromRgb((byte)0, (byte)122, (byte)204));
        private Tuple<int, string> serverData;
        DispatcherTimer Update = new DispatcherTimer();
        DispatcherTimer PressingBind = new DispatcherTimer();
        Connection ServerSide = new Connection();
        Triggerbot Assistance = new Triggerbot();
        


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Storing variables used in UI and for starting threads 
        /// </summary>
        #region Variables
        private SolidColorBrush _EnemyColorPresentation;
        public SolidColorBrush EnemyColorPresentation
        {
            get { return _EnemyColorPresentation; }
            private set
            {
                _EnemyColorPresentation = value;
                OnPropertyChanged("EnemyColorPresentation");
            }
        }

        private SolidColorBrush _FriendColorPresentation;
        public SolidColorBrush FriendColorPresentation
        {
            get { return _FriendColorPresentation; }
            private set
            {
                _FriendColorPresentation = value;
                OnPropertyChanged("FriendColorPresentation");
                FriendRect.Fill = _FriendColorPresentation;
            }
        }

        

        private bool _ToggleTriggerbot;
        public bool ToggleTriggerbot
        {
            get { return _ToggleTriggerbot; }
            private set
            {
                _ToggleTriggerbot = value;
                ThreadValues.Triggerbot = value;
                if (value == true)
                    ToggleTriggerbotButton.BorderBrush = Brushes.Lime;
                else
                    ToggleTriggerbotButton.BorderBrush = Brushes.Red;
            }
        }


        private bool _ToggleWallhack;
        public bool ToggleWallhack
        {
            get { return _ToggleWallhack; }
            private set
            {
                _ToggleWallhack = value;
                ThreadValues.Wallhack = value;
                if (value == true)
                    ToggleWallhackButton.BorderBrush = Brushes.Lime;
                else
                    ToggleWallhackButton.BorderBrush = Brushes.Red;
            }
        }


        private bool _ToggleNoFlash;
        public bool ToggleNoFlash
        {
            get { return _ToggleNoFlash; }
            private set
            {
                _ToggleNoFlash = value;
                ThreadValues.NoFlash = value;
                if (value == true)
                    ToggleNoFlashButton.BorderBrush = Brushes.Lime;
                else
                    ToggleNoFlashButton.BorderBrush = Brushes.Red;
            }
        }


        private bool _ToggleBunnyhop;
        public bool ToggleBunnyhop
        {
            get { return _ToggleBunnyhop; }
            private set
            {
                _ToggleBunnyhop = value;
                if (value == true)
                    ToggleBunnyhopButton.BorderBrush = Brushes.Lime;
                else
                    ToggleBunnyhopButton.BorderBrush = Brushes.Red;
            }
        }

        private int _CurrentlyOnline;
        public int CurrentlyOnline
        {
            get { return _CurrentlyOnline; }
            set
            {
                _CurrentlyOnline = value;
                OnPropertyChanged("CurrentlyOnline");
            }
        }

        private string _DetectionChance;
        public string DetectionChance
        {
            get { return _DetectionChance; }
            set
            {
                _DetectionChance = value;
                OnPropertyChanged("DetectionChance");
                if (value == "UNDETECTED")
                    DetectionBlock.Foreground = Brushes.Green;
                else
                    DetectionBlock.Foreground = Brushes.Red;
            }
        }

        private int _ReactionTime;
        public int ReactionTime
        {
            get { return _ReactionTime; }
            private set
            {
                _ReactionTime = value;
                OnPropertyChanged("ReactionTime");
                if (value < 10)
                    ReactionTimeTextblock.Foreground = Brushes.Lime;
                else if (value >= 10 && value < 25)
                    ReactionTimeTextblock.Foreground = Brushes.Green;
                else if (value >= 25 && value < 35)
                    ReactionTimeTextblock.Foreground = Brushes.Orange;
                else if (value >= 35 && value < 42)
                    ReactionTimeTextblock.Foreground = Brushes.DarkOrange;
                else ReactionTimeTextblock.Foreground = Brushes.Red;
            }
        }


        private float _RedEnemy;
        public float RedEnemy
        {
            get { return _RedEnemy; }
            private set
            {
                _RedEnemy = value;
                OnPropertyChanged("RedEnemy");
                EnemyColorPresentation = new SolidColorBrush(Color.FromRgb((byte)RedEnemy, (byte)GreenEnemy, (byte)BlueEnemy));
            }
        }

        private float _GreenEnemy;
        public float GreenEnemy
        {
            get { return _GreenEnemy; }
            private set
            {
                _GreenEnemy = value;
                OnPropertyChanged("GreenEnemy");
                EnemyColorPresentation = new SolidColorBrush(Color.FromRgb((byte)RedEnemy, (byte)GreenEnemy, (byte)BlueEnemy));
            }
        }

        private float _BlueEnemy;
        public float BlueEnemy
        {
            get { return _BlueEnemy; }
            private set
            {
                _BlueEnemy = value;
                OnPropertyChanged("BlueEnemy");
                EnemyColorPresentation = new SolidColorBrush(Color.FromRgb((byte)RedEnemy, (byte)GreenEnemy, (byte)BlueEnemy));
            }
        }

        private float _RedFriend;
        public float RedFriend
        {
            get { return _RedFriend; }
            private set
            {
                _RedFriend = value;
                OnPropertyChanged("RedFriend");
                FriendColorPresentation = new SolidColorBrush(Color.FromRgb((byte)RedFriend, (byte)GreenFriend, (byte)BlueFriend));
            }
        }

        private float _GreenFriend;
        public float GreenFriend
        {
            get { return _GreenFriend; }
            private set
            {
                _GreenFriend = value;
                OnPropertyChanged("GreenFriend");
                FriendColorPresentation = new SolidColorBrush(Color.FromRgb((byte)RedFriend, (byte)GreenFriend, (byte)BlueFriend));
            }
        }

        private float _BlueFriend;
        public float BlueFriend
        {
            get { return _BlueFriend; }
            private set
            {
                _BlueFriend = value;
                OnPropertyChanged("BlueFriend");
                FriendColorPresentation = new SolidColorBrush(Color.FromRgb((byte)RedFriend, (byte)GreenFriend, (byte)BlueFriend));
            }
        }

        private string _TriggerbotToggleBindName;
        public string TriggerbotToggleBindName
        {
            get { return _TriggerbotToggleBindName; }
            set
            {
                _TriggerbotToggleBindName = value;
                OnPropertyChanged("TriggerbotToggleBindName");
            }
        }

        private string _WallhackToggleBindName;
        public string WallhackToggleBindName
        {
            get { return _WallhackToggleBindName; }
            set
            {
                _WallhackToggleBindName = value;
                OnPropertyChanged("WallhackToggleBindName");
            }
        }

        private string _NoFlashToggleBindName;
        public string NoFlashToggleBindName
        {
            get { return _NoFlashToggleBindName; }
            set
            {
                _NoFlashToggleBindName = value;
                OnPropertyChanged("NoFlashToggleBindName");
            }
        }

        private string _BunnyhopBindName;
        public string BunnyhopBindName
        {
            get { return _BunnyhopBindName; }
            set
            {
                _BunnyhopBindName = value;
                OnPropertyChanged("BunnyhopBindName");
            }
        }

        




        public bool AllowedPistols { get; set; }
        public bool AllowedShotguns { get; set; }
        public bool AllowedAWP { get; set; }
        public bool AllowedRifles { get; set; }
        public bool AllowedSMGs { get; set; }
        public bool AllowedNoobsWeapons { get; set; }
        public bool AllowedQuickScope { get; set; }
        public bool AllowedSwapWeapon { get; set; }
        public bool AllowedAimImprove { get; set; }
        public bool FriendlyWallhack { get; set; }

        private bool IsButtonBindingUnderEdit { get; set; }
        private BindingKey ButtonUnderEdit { get; set; }
        public static int TriggerbotToggleBindKey { get; set; }
        public static int WallhackToggleBindKey { get; set; }
        public static int NoFlashToggleBindKey { get; set; }
        public static int BunnyhopBindKey { get; set; }

        public static bool InjectorThread { get; private set; }
        Thread ThreadInjector;
        Thread ThreadBunnyHop;
        Thread ThreadWeaponAssistance;
        Thread ThreadESP;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            MenuGeneralButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));  // Setting startup UI as GeneralGrid
            Settings.LoadSettings();                                               // Loading user settings used before
            Offsets.LoadOffsetsFromFile();                                         // Loading offsets from file
            this.DataContext = this;                                               // UI Bindings
            KeyDown += new KeyEventHandler(KeyPressKeyBind);                       // Key press event, used for binding keys

            Update.Interval = new TimeSpan(0, 1, 0);                               // Update timer, once per minute taking info from server
            Update.Tick += new EventHandler(Update_tick);
            Update.Start();

            PressingBind.Interval = new TimeSpan(0, 0, 0, 0, 5);
            PressingBind.Tick += new EventHandler(CheckBindPress);
            PressingBind.Start();

            ServerSide.ServerInfo(Connection.ConnectionType.Login);                // Adding one player to counter
            serverData = Parser.ParseResponse(ServerSide.ServerInfo(Connection.ConnectionType.Status)); // Getting current status
            CurrentlyOnline = serverData.Item1;                                    // Setting count of online people
            DetectionChance = serverData.Item2;                                    // Setting detection chance

            ThreadInjector = new Thread(AutoInjector.Inject);                      // Starting thread which one search csgo and inject or re-inject
            InjectorThread = true;
            ThreadInjector.Start();

            ThreadValues.ESP = true;
            ESP ESPerception = new ESP();
            ThreadESP = new Thread(ESPerception.VisualFX);
            ThreadESP.Start();

            ThreadValues.WeaponSupport = true;
            ThreadWeaponAssistance = new Thread(Assistance.WeaponAssistance);
            ThreadWeaponAssistance.Start();

        }


        /// <summary>
        /// Timer to update currently online players and detection chance
        /// </summary>        
        #region Update DispatcherTimer Tick
        private void Update_tick(object sender, EventArgs e)
        {
            serverData = Parser.ParseResponse(ServerSide.ServerInfo(Connection.ConnectionType.Status));
            CurrentlyOnline = serverData.Item1;
            DetectionChance = serverData.Item2;
        }
        #endregion


        #region Checking bind keys
        private void CheckBindPress(object sender, EventArgs e)
        {
            if(IsButtonBindingUnderEdit == false)
            {
                if (Game.GetAsyncKeyState(TriggerbotToggleBindKey) != 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ToggleTriggerbotButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }));
                    Thread.Sleep(150);
                }

                if (Game.GetAsyncKeyState(WallhackToggleBindKey) != 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ToggleWallhackButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }));
                    Thread.Sleep(150);
                }

                if (Game.GetAsyncKeyState(NoFlashToggleBindKey) != 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ToggleNoFlashButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }));
                    Thread.Sleep(150);
                }
            }
            
        }
        #endregion

        /// <summary>
        /// Custom WindowBar and his handling
        /// </summary>        
        #region WindowBar
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Settings.SaveSettings();
            ServerSide.ServerInfo(Connection.ConnectionType.Logout);
            InjectorThread = false;
            ThreadValues.Triggerbot = false;
            ThreadValues.ESP = false;
            ThreadValues.BunnyHop = false;
            ThreadValues.WeaponSupport = false;
            ThreadValues.NoFlash = false;
            ThreadValues.Wallhack = false;
            Update.Stop();
            PressingBind.Stop();
            ThreadWeaponAssistance.Abort();
            Application.Current.Shutdown();
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion


        /// <summary>
        /// ToggleButtons to toggle cheat solutions, setting correct values and start threads
        /// </summary>        
        #region ToggleButtons
        private void ToggleTriggerbotButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleTriggerbot)
            {
                ToggleTriggerbot = false;
                ThreadValues.Triggerbot = false;
            }
                
            else
            {                
                ToggleTriggerbot = true;
                ThreadValues.Triggerbot = true;                
            }
                
        }

        private void ToggleWallhackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleWallhack)
            {
                ToggleWallhack = false;
                ThreadValues.Wallhack = false;
            }

            else
            {
                ToggleWallhack = true;
                ThreadValues.Wallhack = true;
            }
                
        }

        private void ToggleNoFlashButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleNoFlash)
            {
                ToggleNoFlash = false;
                ThreadValues.NoFlash = false;
            }                

            else
            {
                ToggleNoFlash = true;
                ThreadValues.NoFlash = true;                
            }
        }

        private void ToggleBunnyhopButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleBunnyhop)
            {
                ToggleBunnyhop = false;
                ThreadValues.BunnyHop = false; 
            }
                
            else
            {
                ToggleBunnyhop = true;
                ThreadValues.BunnyHop = true;
                ThreadBunnyHop = new Thread(BunnyHop.GetJump);
                ThreadBunnyHop.Start();
            }
                
        }
        #endregion


        /// <summary>
        /// Setting correct Grids and colors after clicking chosen button
        /// </summary>        
        #region ChangeContent
        private void ChangeGridContent(Grid Set,Button Pressed)
        {
            GeneralGrid.Visibility = Visibility.Hidden;
            TriggerbotGrid.Visibility = Visibility.Hidden;
            WallhackGrid.Visibility = Visibility.Hidden;
            OtherGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Hidden;

            MenuGeneralButton.Background = null;
            MenuTriggerbotButton.Background = null;
            MenuWallhackButton.Background = null;
            MenuOtherButton.Background = null;
            MenuSettingsButton.Background = null;
            
            Pressed.Background = CustomBlue;
            Set.Visibility = Visibility.Visible;
        }

        private void MenuGeneralButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGridContent(GeneralGrid, MenuGeneralButton);
        }

        private void MenuTriggerbotButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGridContent(TriggerbotGrid, MenuTriggerbotButton);
        }

        private void MenuWallhackButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGridContent(WallhackGrid, MenuWallhackButton);
        }

        private void MenuOtherButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGridContent(OtherGrid, MenuOtherButton);
        }

        private void MenuSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeGridContent(SettingsGrid, MenuSettingsButton);
        }
        #endregion

        /// <summary>
        /// Setting Triggerbot options
        /// </summary>        
        #region Triggerbot Variable Changers
        private void ReactionTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ReactionTime = (int)ReactionTimeSlider.Value;
            ThreadValues.ReactionTime = ReactionTime;
        }


        private void NoobsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (NoobsCheckbox.IsChecked == true)
            {
                AllowedNoobsWeapons = true;
                ThreadValues.NoobWeapons = true;
            }               

            else
            {
                AllowedNoobsWeapons = false;
                ThreadValues.NoobWeapons = false;
            }
                
        }

        private void SMGsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (SMGsCheckbox.IsChecked == true)
            {
                AllowedSMGs = true;
                ThreadValues.SMGs = true;
            }
                
            else
            {
                AllowedSMGs = false;
                ThreadValues.SMGs = false;
            }
                
        }

        private void RiflesCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (RiflesCheckbox.IsChecked == true)
            {
                AllowedRifles = true;
                ThreadValues.Rifles = true;
            }
                
            else
            {
                AllowedRifles = false;
                ThreadValues.Rifles = false;
            }
                
        }

        private void ShotgunsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (ShotgunsCheckBox.IsChecked == true)
            {
                AllowedShotguns = true;
                ThreadValues.Shotguns = true;
            }
            else
            {
                AllowedShotguns = false;
                ThreadValues.Shotguns = false;
            }
                
        }

        private void AWPCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (AWPCheckbox.IsChecked == true)
            {
                AllowedAWP = true;
                ThreadValues.AWP = true;
            }

            else
            {
                AllowedAWP = false;
                ThreadValues.AWP = false;
            }
                
        }

        private void PistolsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (PistolsCheckbox.IsChecked == true)
            {
                AllowedPistols = true;
                ThreadValues.Pistols = true;
            }

            else
            {
                AllowedPistols = false;
                ThreadValues.Pistols = false;
            }
        }
        #endregion

        /// <summary>
        /// Setting Wallhack output colors
        /// </summary>        
        #region Color Selection Sliders
        private void RedEnemySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedEnemy = (float)RedEnemySlider.Value;
            ThreadValues.EnemyRed = (float)RedEnemySlider.Value;
        }

        private void GreenEnemySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GreenEnemy = (float)GreenEnemySlider.Value;
            ThreadValues.EnemyGreen = (float)GreenEnemySlider.Value;
        }

        private void BlueEnemySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlueEnemy = (float)BlueEnemySlider.Value;
            ThreadValues.EnemyBlue = (float)BlueEnemySlider.Value;
        }

        private void RedFriendSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedFriend = (float)RedFriendSlider.Value;
            ThreadValues.FriendRed = (float)RedFriendSlider.Value;
        }

        private void GreenFriendSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GreenFriend = (float)GreenFriendSlider.Value;
            ThreadValues.FriendGreen = (float)GreenFriendSlider.Value;
        }

        private void BlueFriendSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlueFriend = (float)BlueFriendSlider.Value;
            ThreadValues.FriendBlue = (float)BlueFriendSlider.Value;
        }
        #endregion

        /// <summary>
        /// Other stuff
        /// </summary>
        #region Other Cheats Grid
        private void QuickScopeCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (QuickScopeCheckbox.IsChecked == true)
            {
                AllowedQuickScope = true;
                ThreadValues.QuickScope = true;
            }

            else
            {
                AllowedQuickScope = false;
                ThreadValues.QuickScope = false;
            }
                
        }

        private void ReloadChangeCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (ReloadChangeCheckbox.IsChecked == true)
            {
                AllowedSwapWeapon = true;
                ThreadValues.WeaponChangeAtReload = true;
            }

            else
            {
                AllowedSwapWeapon = false;
                ThreadValues.WeaponChangeAtReload = false;
            }
        }

        private void AccuracyImproveCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (AccuracyImproveCheckbox.IsChecked == true)
            {
                AllowedAimImprove = true;
                ThreadValues.FireControl = true;
            }

            else
            {
                AllowedAimImprove = false;
                ThreadValues.FireControl = false;
            }
        }
        #endregion

        /// <summary>
        /// Binding keys for use in game
        /// </summary>
        #region KeyBinding
        private void KeyPressKeyBind(object sender, KeyEventArgs e)
        {
            if(IsButtonBindingUnderEdit)
            {
                switch(ButtonUnderEdit)
                {
                    case BindingKey.Triggerbot:
                        TriggerbotToggleBindName = e.Key.ToString();
                        TriggerbotToggleBindKey = KeyInterop.VirtualKeyFromKey(e.Key);
                        break;

                    case BindingKey.Wallhack:
                        WallhackToggleBindName = e.Key.ToString();
                        WallhackToggleBindKey = KeyInterop.VirtualKeyFromKey(e.Key);
                        break;

                    case BindingKey.NoFlash:
                        NoFlashToggleBindName = e.Key.ToString();
                        NoFlashToggleBindKey = KeyInterop.VirtualKeyFromKey(e.Key);
                        break;

                    case BindingKey.Bunnyhop:
                        BunnyhopBindName = e.Key.ToString();
                        BunnyhopBindKey = KeyInterop.VirtualKeyFromKey(e.Key);
                        break;
                }                
                IsButtonBindingUnderEdit = false;
            }
        }
        #endregion
        
        /// <summary>
        /// Rest of settings, handling buttons
        /// </summary>
        #region Settings Grid

        private enum BindingKey
        {
            Triggerbot,
            Wallhack,
            NoFlash,
            Bunnyhop
        }

        private void TriggerbotKey_Click(object sender, RoutedEventArgs e)
        {
            IsButtonBindingUnderEdit = true;
            ButtonUnderEdit = BindingKey.Triggerbot;
            TriggerbotToggleBindName = "Bind";

        }

        private void WallhackKey_Click(object sender, RoutedEventArgs e)
        {
            IsButtonBindingUnderEdit = true;
            ButtonUnderEdit = BindingKey.Wallhack;
            WallhackToggleBindName = "Bind";
        }

        private void NoFlashKey_Click(object sender, RoutedEventArgs e)
        {
            IsButtonBindingUnderEdit = true;
            ButtonUnderEdit = BindingKey.NoFlash;
            NoFlashToggleBindName = "Bind";
        }

        private void BunnyhopKey_Click(object sender, RoutedEventArgs e)
        {
            IsButtonBindingUnderEdit = true;
            ButtonUnderEdit = BindingKey.Bunnyhop;
            BunnyhopBindName = "Bind";
        }
                

        private void FriendlyWallhackCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (FriendlyWallhackCheckbox.IsChecked == true)
            {
                FriendlyWallhack = true;
                ThreadValues.FriendlyWallhack = true;
            }
                
            else
            {
                FriendlyWallhack = false;
                ThreadValues.FriendlyWallhack = false;
            }
                
        }

        private void GetOffsetsButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
