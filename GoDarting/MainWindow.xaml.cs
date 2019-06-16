using System.Windows;
using GoDartingUserControls;
using GoDartingLogic;
using System.Threading;

namespace GoDarting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //StartupScreen ucStartupScreen = null;
        TargetSelection ucTargetSelect=null;
        PlayingScreen ucPlayScreen = null;
        LevelScreen ucLevelScreen = null;
        FinalScreen ucFinalScreen = null;
        PlayerRankScreen ucPlayerRankScreen = null;
        ProfileScreen ucProfileScreen = null;
        RegisterScreen ucRegisterScreen = null;
        UserLoginScreen ucUserLoginScreen = null;
        CustomTargetScreen ucCustomTargetscreen = null;
        Thread thread = null;
        Music music;
        System.Windows.Style msgStyle;

        public MainWindow()
        {
            InitializeComponent();
            msgStyle = this.TryFindResource("MessageBoxStyle") as System.Windows.Style;
            ChangeUI.switchScreenEvent+=new switchScreen(SwitchScreenfromUI);
            Game.showMessageEvent += new showMessage(Game_showMessageEvent);
            Game.ShowProgressBarEvent += new ShowProgressBar(Game_ShowProgressBarEvent);
            Game.HideProgressBarEvent += new HideProgressBar(Game_HideProgressBarEvent);
            Game.ToggleSoundEvent += new ToggleSound(Game_ToggleSoundEvent);
            music = new Music();
            thread = new Thread( new ThreadStart(music.StartMusic));
            thread.IsBackground = true;
            thread.Start();
        }

        void Game_ToggleSoundEvent(bool sound)
        {
            if (!sound)
                music.PauseMusic();
            else
                music.ResumeMusic();
            
        }

        void Game_HideProgressBarEvent()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                progressBar.Visibility = Visibility.Hidden;
            });

           
        }

        void Game_ShowProgressBarEvent()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                progressBar.Visibility = Visibility.Visible;
            });     
            
        }

        void Game_showMessageEvent(string message)
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(message, "GoDarting Information", MessageBoxButton.OK,msgStyle);
            });
            
        }

        public void SwitchScreenfromUI(ScreenName screenToRemove, ScreenName screenToAdd)
        {
            RemoveScreenFromUI(screenToRemove);
            AddScreenToUI(screenToAdd);
            
        }

        private void RemoveScreenFromUI(ScreenName screenToRemove)
        {
            if (screenToRemove == ScreenName.StartScreen)
            {                
                mainGrid.Children.Remove(startupScreen);                
            }

            else if (screenToRemove == ScreenName.TargetScreen)
            {                
                mainGrid.Children.Remove(ucTargetSelect);
            }
            else if (screenToRemove == ScreenName.LevelScreen)
            {
                mainGrid.Children.Remove(ucLevelScreen);
            }

            else if (screenToRemove == ScreenName.PlayingScreen)
            {
                mainGrid.Children.Remove(ucPlayScreen);
            }


            else if (screenToRemove == ScreenName.PlayerRankScreen)
            {
                mainGrid.Children.Remove(ucPlayerRankScreen);
            }



            else if (screenToRemove == ScreenName.ProfileScreen)
            {
                mainGrid.Children.Remove(ucProfileScreen);
            }


            else if (screenToRemove == ScreenName.FinalScreen)
            {
                mainGrid.Children.Remove(ucFinalScreen);
            }


            else if (screenToRemove == ScreenName.RegisterScreen)
            {
                mainGrid.Children.Remove(ucRegisterScreen);
            }

            
            else if (screenToRemove == ScreenName.UserLoginScreen)
            {
                mainGrid.Children.Remove(ucUserLoginScreen);
            }

            else if (screenToRemove == ScreenName.CustomTargetScreen)
            {
                mainGrid.Children.Remove(ucCustomTargetscreen);
            }
        }

        private void AddScreenToUI(ScreenName screenToAdd)
        {
            if (screenToAdd == ScreenName.StartScreen)
            {
                startupScreen = new StartupScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(startupScreen);
                mainGrid.Children.Add(progressBar);

            }

            else if (screenToAdd == ScreenName.TargetScreen)
            {
                ucTargetSelect = new TargetSelection();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucTargetSelect);
                mainGrid.Children.Add(progressBar);

            }
            else if (screenToAdd == ScreenName.LevelScreen)
            {
                ucLevelScreen = new LevelScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucLevelScreen);
                mainGrid.Children.Add(progressBar);

            }

            else if (screenToAdd == ScreenName.PlayingScreen)
            {
                ucPlayScreen = new PlayingScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucPlayScreen);
                mainGrid.Children.Add(progressBar);
            }

            else if (screenToAdd == ScreenName.FinalScreen)
            {
                ucFinalScreen = new FinalScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucFinalScreen);
                mainGrid.Children.Add(progressBar);
            }

            else if (screenToAdd == ScreenName.ProfileScreen)
            {
                ucProfileScreen = new ProfileScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucProfileScreen);
                mainGrid.Children.Add(progressBar);
            }

            else if (screenToAdd == ScreenName.UserLoginScreen)
            {
                ucUserLoginScreen = new UserLoginScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucUserLoginScreen);
                mainGrid.Children.Add(progressBar);
            }

            else if (screenToAdd == ScreenName.RegisterScreen)
            {
                ucRegisterScreen = new RegisterScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucRegisterScreen);
                mainGrid.Children.Add(progressBar);
            }

            else if (screenToAdd == ScreenName.PlayerRankScreen)
            {
                ucPlayerRankScreen = new PlayerRankScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucPlayerRankScreen);
                mainGrid.Children.Add(progressBar);
            }

            else if (screenToAdd == ScreenName.CustomTargetScreen)
            {
                ucCustomTargetscreen = new CustomTargetScreen();
                mainGrid.Children.Remove(progressBar);
                mainGrid.Children.Add(ucCustomTargetscreen);
                mainGrid.Children.Add(progressBar);
            }
        }
        
    }
}
