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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for CustomTargetScreen.xaml
    /// </summary>
    public partial class CustomTargetScreen : UserControl
    {
        public CustomTargetScreen()
        {
            InitializeComponent();
            RemovePreviousCustomImages();
            var image = System.Drawing.Image.FromFile(ChangeUI.DartBoardImage);
            var newImage = ChangeUI.ScaleImage(image, 600, 300);
            string path = Directory.GetCurrentDirectory();
            path += @"\customface0.jpg";
            newImage.Save(path, ImageFormat.Jpeg);            
            //customImage.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
            //customImage.Width = newImage.Width;
            //customImage.Height = newImage.Height;
            this.ImageCropper.ImageUrl = path;
            this.ImageCropper.Width = newImage.Width;
            this.ImageCropper.Height = newImage.Height;
        }

        private void RemovePreviousCustomImages()
        {
            List<string> filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg").ToList();
            foreach (var fileName in filePaths)
            {
                if (fileName.Contains("customface"))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (Exception e)
                    { 
                        
                    }
                }
            }
        }

        

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            string customface = "";
            List<string> filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg").ToList();

            //
            foreach (var fileName in filePaths)
            {
                
                if (fileName.Contains("customface"))
                {
                    if(String.Compare(customface, fileName)<0)
                        customface=fileName;
                }
            }
            var image = System.Drawing.Image.FromFile(customface);
            var newImage = ChangeUI.ScaleImage(image, 80, 100);
            File.Delete(Directory.GetCurrentDirectory() + @"\latestphoto.jpg");
            newImage.Save(Directory.GetCurrentDirectory()+@"\latestphoto.jpg", ImageFormat.Jpeg);
            ChangeUI.DartBoardImage = Directory.GetCurrentDirectory() + @"\latestphoto.jpg";
            ChangeUI.RemoveScreen(ScreenName.CustomTargetScreen, ScreenName.LevelScreen);
        }
    }
}
