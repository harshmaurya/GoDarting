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
using System.Threading;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for UserLoginScreen.xaml
    /// </summary>
    public partial class UserLoginScreen : UserControl
    {
        public UserLoginScreen()
        {
            InitializeComponent();
            Game.AuthenticationCompleteEvent += new AuthenticationComplete(Game_AuthenticationCompleteEvent);
            
        }

       


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.StartScreen, ScreenName.RegisterScreen);
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.UserLoginScreen, ScreenName.StartScreen);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            Game.AuthenticateUser(txtUsername.Text, txtPassword.Password);
           
        }

        void Game_AuthenticationCompleteEvent()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                if (Game.CurrentPlayer != null)
                {
                    if (Game.FinalScore != 0)
                        Game.SubmitScore();
                    ChangeUI.RemoveScreen(ScreenName.UserLoginScreen, ScreenName.ProfileScreen);
                }
                else
                    Game.RaiseMessageBoxEvent("Username or password is incorrect");
            });

           
        }

       
    }
}
