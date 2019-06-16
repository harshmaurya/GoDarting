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
    /// Interaction logic for ProfileScreen.xaml
    /// </summary>
    public partial class ProfileScreen : UserControl
    {

        public ProfileScreen()
        {
            InitializeComponent();
            if (Game.CurrentPlayer != null)
            {
                lblEmail.Content = Game.CurrentPlayer.EmailId;
                lblFirstName.Content = Game.CurrentPlayer.PlayerFirstName;
                lblHighestScore.Content = Game.CurrentPlayer.HighestScore;
                lblPassword.Content = "*******";                
                lblUsername.Content = Game.CurrentPlayer.PlayerUserName;
                lblRank.Content = Game.PlayerRank;
                lblTotal.Content = Game.TotalPlayers;               
            }

            else
            {
                txtGuestMessage.Visibility = Visibility.Visible;
                lblEmail.Content = "N/A";
                lblFirstName.Content = "Guest";
                lblHighestScore.Content = "N/A";
                lblPassword.Content = "N/A";
                lblUsername.Content = "N/A";
                lblRank.Content = "";
                lblTotal.Content = "";
            }
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.ProfileScreen, ScreenName.StartScreen);
        }
    }
}
