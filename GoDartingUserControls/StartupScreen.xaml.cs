using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GoDartingLogic;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StartupScreen : UserControl
    {
        bool sound = true;
        public StartupScreen()
        {
            InitializeComponent();
            Game.ResetGame();
            if (Game.CurrentPlayer != null)
                btnLogin.Content = "Logout";
            
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.StartScreen,ScreenName.TargetScreen);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Game.CurrentPlayer == null)
                ChangeUI.RemoveScreen(ScreenName.StartScreen, ScreenName.UserLoginScreen);
            else
            {
                Game.CurrentPlayer = null;
                Game.RaiseMessageBoxEvent("You have been logged out!!");
                btnLogin.Content = "Login";
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.StartScreen, ScreenName.ProfileScreen);
        }

        private void btnTopPlayers_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.StartScreen, ScreenName.PlayerRankScreen);
        }

        private void btnSpeaker_Click(object sender, RoutedEventArgs e)
        {
            sound = !sound;
            toggleSpeakerImage();
            toggleSound();
        }

        private void toggleSound()
        {
            Game.ToggleSound(sound);
        }

        private void toggleSpeakerImage()
        {
            if (sound)
                imgSpeaker.Source = new BitmapImage(new Uri(@"Images\speaker.png", UriKind.Relative));
            else
                imgSpeaker.Source = new BitmapImage(new Uri(@"Images\nospeaker.png", UriKind.Relative));    
        }
    }
}
