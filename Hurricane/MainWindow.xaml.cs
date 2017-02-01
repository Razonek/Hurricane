using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Hurricane
{
    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        SolidColorBrush CustomBlue = new SolidColorBrush(Color.FromRgb((byte)0, (byte)122, (byte)204));

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



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }



        private bool _ToggleTriggerbot;
        public bool ToggleTriggerbot
        {
            get { return _ToggleTriggerbot; }
            private set
            {
                _ToggleTriggerbot = value;
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






        public bool AllowedPistols { get; private set; }
        public bool AllowedShotguns { get; private set; }
        public bool AllowedAWP { get; private set; }
        public bool AllowedRifles { get; private set; }
        public bool AllowedSMGs { get; private set; }
        public bool AllowedNoobsWeapons { get; private set; }






        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            CurrentlyOnline = 17;
            DetectionChance = "UNDETECTED";
            MenuGeneralButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }

        #region WindowBar
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        #region ToggleButtons
        private void ToggleTriggerbotButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleTriggerbot)
                ToggleTriggerbot = false;
            else
                ToggleTriggerbot = true;
        }

        private void ToggleWallhackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleWallhack)
                ToggleWallhack = false;
            else
                ToggleWallhack = true;
        }

        private void ToggleNoFlashButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleNoFlash)
                ToggleNoFlash = false;
            else
                ToggleNoFlash = true;
        }

        private void ToggleBunnyhopButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleBunnyhop)
                ToggleBunnyhop = false;
            else
                ToggleBunnyhop = true;
        }
        #endregion

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

        #region Triggerbot Variable Changers
        private void ReactionTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ReactionTime = (int)ReactionTimeSlider.Value;
        }


        private void NoobsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (NoobsCheckbox.IsChecked == true)
                AllowedNoobsWeapons = true;
            else
                AllowedNoobsWeapons = false;
        }

        private void SMGsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (SMGsCheckbox.IsChecked == true)
                AllowedSMGs = true;
            else
                AllowedSMGs = false;
        }

        private void RiflesCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (RiflesCheckbox.IsChecked == true)
                AllowedRifles = true;
            else
                AllowedRifles = false;
        }

        private void ShotgunsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (ShotgunsCheckBox.IsChecked == true)
                AllowedShotguns = true;
            else
                AllowedShotguns = false;
        }

        private void AWPCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (AWPCheckbox.IsChecked == true)
                AllowedAWP = true;
            else
                AllowedAWP = false;
        }

        private void PistolsCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (PistolsCheckbox.IsChecked == true)
                AllowedPistols = true;
            else
                AllowedPistols = false;
        }
        #endregion

        #region Color Selection Sliders
        private void RedEnemySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedEnemy = (float)RedEnemySlider.Value;
        }

        private void GreenEnemySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GreenEnemy = (float)GreenEnemySlider.Value;
        }

        private void BlueEnemySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlueEnemy = (float)BlueEnemySlider.Value;
        }

        private void RedFriendSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedFriend = (float)RedFriendSlider.Value;
        }

        private void GreenFriendSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GreenFriend = (float)GreenFriendSlider.Value;
        }

        private void BlueFriendSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlueFriend = (float)BlueFriendSlider.Value;
        }
        #endregion
    }
}
