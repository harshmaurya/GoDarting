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
    /// Interaction logic for PlayerRankScreen.xaml
    /// </summary>
    public partial class PlayerRankScreen : UserControl
    {
        public PlayerRankScreen()
        {
            InitializeComponent();
            Game.Top20PlayersFetchedEvent += new Top20PlayersFetched(Game_Top20PlayersFetchedEvent);
        }


        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.PlayerRankScreen, ScreenName.StartScreen);
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (Game.Top20Players == null)
                Game.GetTop20Players();
            else
                dataGridToppers.ItemsSource = Game.Top20Players;
        }

        void Game_Top20PlayersFetchedEvent()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                dataGridToppers.ItemsSource = Game.Top20Players;
            });
           
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            Game.GetTop20Players();
        }
        
    }
}
