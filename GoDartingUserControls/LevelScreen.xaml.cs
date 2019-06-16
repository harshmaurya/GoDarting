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
    /// Interaction logic for LevelScreen.xaml
    /// </summary>
    public partial class LevelScreen : UserControl
    {
        public LevelScreen()
        {
            InitializeComponent();
            txtLevel.Text = Game.CurrentLevel.ToString();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.RemoveScreen(ScreenName.LevelScreen,ScreenName.PlayingScreen);
        }
    }
}
