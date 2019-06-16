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
using System.IO;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for TargetSelection.xaml
    /// </summary>
    public partial class TargetSelection : UserControl
    {
        string recentImages = null;
        BitmapImage recentPhoto=null;
        public TargetSelection()
        {
            InitializeComponent();
            recentImages = Directory.GetFiles(Directory.GetCurrentDirectory(), "latestphoto.jpg").ToList().FirstOrDefault();
            if (recentImages != null)
            {
                
                    BitmapImage image = new BitmapImage();
                    using (FileStream stream = File.OpenRead(recentImages))
                    {
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                    }
                    recentPhoto = image;
                //recentPhoto = new BitmapImage(new Uri(recentImages, UriKind.Absolute));
                recentImage.Source = recentPhoto;
                
            }

        }

        private void btnObama_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.DartBoardImage = @"Images\boardobama.png";
            ChangeUI.IsCustomImage = false;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen,ScreenName.LevelScreen);
            
        }

        private void btnkohli_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.DartBoardImage = @"Images\boardkohli.png";
            ChangeUI.IsCustomImage = false;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.LevelScreen);
        }

        private void btnSymonds_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.DartBoardImage = @"Images\boardsymonds.png";
            ChangeUI.IsCustomImage = false;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.LevelScreen);
        }

        private void btnBrad_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.DartBoardImage = @"Images\boardbrad.png";
            ChangeUI.IsCustomImage = false;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.LevelScreen);
        }

        private void btnHarsh_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.DartBoardImage = @"Images\boardharsh.png";
            ChangeUI.IsCustomImage = false;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.LevelScreen);
        }

        private void btnManmohan_Click(object sender, RoutedEventArgs e)
        {
            ChangeUI.DartBoardImage = @"Images\boardmanmohan.png";
            ChangeUI.IsCustomImage = false;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.LevelScreen);
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            List<string> allowableFileTypes = new List<string>();
            allowableFileTypes.AddRange(new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" });
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!ofd.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(ofd.FileName);
                    if (allowableFileTypes.Contains(f.Extension.ToLower()))
                    {
                        ChangeUI.DartBoardImage = f.FullName;
                        ChangeUI.IsCustomImage = true;
                        ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.CustomTargetScreen);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file type");
                    }
                }
                else
                {
                    MessageBox.Show("You did pick a file to use");
                }
            }
        }

      
        private void btnRecent_Click(object sender, RoutedEventArgs e)
        {
            
            if(recentImages!=null)
            {
            ChangeUI.DartBoardImage = recentImages;
            ChangeUI.IsCustomImage = true;
            ChangeUI.RemoveScreen(ScreenName.TargetScreen, ScreenName.LevelScreen);
            }
        }

       
    }
}
