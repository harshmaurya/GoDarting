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
using GoDartingLogic.PlayerDetailServiceReference;
using System.Threading;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : UserControl
    {
        public RegisterScreen()
        {
            InitializeComponent();
            Game.CheckUsernameEvent += new CheckUsername(Game_CheckUsernameEvent);
            Game.RegisterPlayerEvent += new RegisterPlayer(Game_RegisterPlayerEvent);
        }

       
      

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.RegisterScreen, ScreenName.StartScreen);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            PlayerDetail player = new PlayerDetail
                                       {
                                           EmailId = txtEmail.Text,
                                           HighestScore = Game.FinalScore,
                                           Password = Game.Encrypt(txtPassword.Password),
                                           PlayerFirstName = txtFirstName.Text,
                                           PlayerUserName = txtUsername.Text
                                       };
            Game.RegisterPlayer(player);
                
            

        }

        void Game_RegisterPlayerEvent()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                ChangeUI.RemoveScreen(ScreenName.RegisterScreen, ScreenName.StartScreen);
            });
           
        }


        private void btnUsernameAvaialability_Click(object sender, RoutedEventArgs e)
        {
            Game.Checkusername(txtUsername.Text);
        }

        void Game_CheckUsernameEvent()
        {
            
        }
    }
}
