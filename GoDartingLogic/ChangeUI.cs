using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GoDartingLogic
{    
    public delegate void switchScreen(ScreenName screenToRemove, ScreenName screenToAdd);

    public static class ChangeUI
    {
        public static string DartBoardImage { get; set; }

        public static bool IsCustomImage { get; set; }

        public static event switchScreen switchScreenEvent;
        public static void RemoveScreen(ScreenName screenToRemove, ScreenName screenToAdd)
        {
            if (switchScreenEvent != null)
            {
               switchScreenEvent(screenToRemove, screenToAdd);
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}
