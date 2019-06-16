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
using System.IO;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for FinalScreen.xaml
    /// </summary>
    public partial class FinalScreen : UserControl
    {
        Random rand;
        List<string> badComments;
        List<string> goodComments;
        BitmapImage targetPic = null;

        public FinalScreen()
        {
            InitializeComponent();
            badComments = new List<string> { 
                            "You are such a noob ! Go, cry to your mommy.",
                            "Your kid may be an honors student, but you're still an idiot.",
                            "Go hang yourself, you naughty mocking uncle!",
                            "You must have got a license for being that moron.",
                            "Are you always this stupid or are you making a special effort today?"
                            };

            goodComments = new List<string> { 
                            "You frighten me like hell. Forgive me please.",
                            "You are awesome. Got the sh*t out of me.",
                            "How do you manage such an awesome personality?",
                            "I'll do anything to spend just a few moments with you.",
                            "I think you should have a try in Olympics."
                            };
            txtFinalScore.Text = Game.FinalScore.ToString();
            ConfigureLayout();

            Game.ScoreSubmitedEvent += new ScoreSubmited(Game_ScoreSubmitedEvent);
           
        }

        private void ConfigureLayout()
        {
            rand = new Random();
            int commentIndex = rand.Next(badComments.Count);

            if (!ChangeUI.IsCustomImage)
            {

                if (ChangeUI.DartBoardImage.Contains("obama"))
                    targetImage.Source = new BitmapImage(new Uri(@"Images\obamacomment.png", UriKind.Relative));

                else if (ChangeUI.DartBoardImage.Contains("brad"))
                    targetImage.Source = new BitmapImage(new Uri(@"Images\bradcomment.png", UriKind.Relative));

                else if (ChangeUI.DartBoardImage.Contains("symonds"))
                    targetImage.Source = new BitmapImage(new Uri(@"Images\symondscomment.png", UriKind.Relative));

                else if (ChangeUI.DartBoardImage.Contains("kohli"))
                    targetImage.Source = new BitmapImage(new Uri(@"Images\kohlicomment.png", UriKind.Relative));

                else if (ChangeUI.DartBoardImage.Contains("manmohan"))
                    targetImage.Source = new BitmapImage(new Uri(@"Images\manmohancomment.png", UriKind.Relative));

                else if (ChangeUI.DartBoardImage.Contains("harsh"))
                    targetImage.Source = new BitmapImage(new Uri(@"Images\harshcomment.png", UriKind.Relative));

            }

            else
            {
                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(Directory.GetCurrentDirectory() + @"\latestphoto.jpg"))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                targetPic = image;
                //recentPhoto = new BitmapImage(new Uri(recentImages, UriKind.Absolute));
                targetImage.Source = targetPic;

                //targetImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory()+@"\latestphoto.jpg", UriKind.Absolute));
            
            
            }

            if (Game.FinalScore < 1500)
            {
                imgThumbs.Source = new BitmapImage(new Uri(@"Images\thumbsdown.png", UriKind.Relative));
                txtblockballoon.Text = badComments[commentIndex];
            }
            else
            {

                if (ChangeUI.DartBoardImage.Contains("harsh"))
                {
                    txtblockballoon.Text = badComments[commentIndex];
                    imgThumbs.Source = new BitmapImage(new Uri(@"Images\thumbsdown.png", UriKind.Relative));
                }
                else
                {
                    txtblockballoon.Text = goodComments[commentIndex];
                    imgThumbs.Source = new BitmapImage(new Uri(@"Images\thumbsup.png", UriKind.Relative));
                }
            }
            
        }

        
                
        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Game.ResetGame();
            ChangeUI.RemoveScreen(ScreenName.FinalScreen, ScreenName.StartScreen);
        }

        private void btnSubmitScore_Click(object sender, RoutedEventArgs e)
        {
            if (Game.CurrentPlayer == null)
                ChangeUI.RemoveScreen(ScreenName.FinalScreen, ScreenName.UserLoginScreen);
            else
            {
                Game.SubmitScore();                
            }
        }

        void Game_ScoreSubmitedEvent()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                ChangeUI.RemoveScreen(ScreenName.FinalScreen, ScreenName.StartScreen);
            });
           
        }

       
    }
}
