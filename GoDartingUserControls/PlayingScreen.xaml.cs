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
using System.Windows.Media.Animation;
using System.Threading;
using GoDartingLogic;
using System.Drawing;
using System.Windows.Threading;
using System.IO;

namespace GoDartingUserControls
{
    /// <summary>
    /// Interaction logic for PlayingScreen.xaml
    /// </summary>
    public partial class PlayingScreen : UserControl
    {
        Storyboard sBoardHorizontal =null;
        Storyboard sBoardVertical = null;
        Storyboard sBoardDart = null;
        Storyboard sScoreAnim = null;
        DoubleAnimation dartAnimationHorizontal = null;
        DoubleAnimation dartAnimationVertical = null;
        DoubleAnimation anim = null;
        DoubleAnimation anim2 = null;
        DoubleAnimation scoreAnimation = null;
        DoubleAnimation scoreAnimation2 = null;
        DoubleAnimationUsingKeyFrames keyFramehorizontal = null;
        DoubleAnimationUsingKeyFrames keyFramevertical = null;
        DispatcherTimer WaitAfterDartAnimationTimer;        
        DispatcherTimer WaitAfterHittingScoreDisplay;
        BitmapImage dartBoardPhoto = null;

        public PlayingScreen()
        {
            InitializeComponent();
            SetUIVariables();
            this.Cursor = Cursors.Hand;
        }

        private void ConfigureTimers()
        {
            WaitAfterDartAnimationTimer = new DispatcherTimer();
            WaitAfterDartAnimationTimer.Tick += new EventHandler(WaitAfterDartAnimationTimer_Tick);
            WaitAfterDartAnimationTimer.Interval = new TimeSpan(0, 0, 1);

           

            WaitAfterHittingScoreDisplay = new DispatcherTimer();
            WaitAfterHittingScoreDisplay.Tick += new EventHandler(WaitAfterHittingScoreDisplay_Tick);
            WaitAfterHittingScoreDisplay.Interval = new TimeSpan(0, 0, 1);
        }

       

        private void SetUIVariables()
        {
            Uri photoPath;
            if (!ChangeUI.IsCustomImage)
            {
                photoPath = new Uri(ChangeUI.DartBoardImage, UriKind.Relative);
                BitmapImage img = new BitmapImage(photoPath);
                dartBoardImage.Source = img;
            }
            else
            {
                //photoPath = new Uri(ChangeUI.DartBoardImage, UriKind.Absolute);
                dartBoardImage.Visibility = Visibility.Hidden;
                customDartBoard.Visibility = Visibility.Visible;
                pinImage.Visibility = Visibility.Visible;


                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(ChangeUI.DartBoardImage))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                dartBoardPhoto = image;
                //recentPhoto = new BitmapImage(new Uri(recentImages, UriKind.Absolute));
                customPhoto.Source = dartBoardPhoto;

                //customPhoto.Source = new BitmapImage(photoPath);


                customPhoto.Visibility = Visibility.Visible;
            }
           
            
            lblLevel.Content = Game.CurrentLevel;
            lblRequiredScore.Content = Game.PointsToClearLevel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigureHAnimation();
            ConfigureVAnimation();
            ConfigureTimers();
            sBoardHorizontal.Begin(mainGrid, true);
            sBoardVertical.Begin(mainGrid, true);
            sBoardVertical.Pause(mainGrid);
            SetGameGeometry();
        }

        private void SetGameGeometry()
        {
            GameGeometry.HorizontalBar = new System.Drawing.Point { X = Convert.ToInt32(Canvas.GetLeft(horizontalBar))-10, Y = Convert.ToInt32(Canvas.GetTop(horizontalBar)) };  // -10 for dart adjustments
            GameGeometry.VerticalBar = new System.Drawing.Point { X = Convert.ToInt32(Canvas.GetLeft(verticalBar)), Y = Convert.ToInt32(Canvas.GetTop(verticalBar))-10 };
            GameGeometry.HorizontalBarLength = Convert.ToInt32(horizontalBar.Width);
            GameGeometry.VerticalBarLength = Convert.ToInt32(verticalBar.Height);
            GameGeometry.DartBoard = new System.Drawing.Point { X = Convert.ToInt32(Canvas.GetLeft(dartBoardImage)), Y = Convert.ToInt32(Canvas.GetTop(dartBoardImage)) };
            GameGeometry.DartBoardLength = Convert.ToInt32(dartBoardImage.Height);
            GameGeometry.CalculateAll();
        }


        private void ConfigureHAnimation()
        {
            sBoardHorizontal = new Storyboard();            
            if (Game.CurrentLevel < 6 )
            {
                anim = new DoubleAnimation(190, 580, TimeSpan.FromMilliseconds(Game.PointerTime));
                Storyboard.SetTargetName(anim, hPointer.Name);
                Storyboard.SetTargetProperty(anim, new PropertyPath(Canvas.LeftProperty));
                sBoardHorizontal.Children.Add(anim);
            }
            else
            {
                keyFramehorizontal = new DoubleAnimationUsingKeyFrames();
                keyFramehorizontal.Duration = TimeSpan.FromMilliseconds(Game.PointerTime);
                keyFramehorizontal.KeyFrames.Add(
                new SplineDoubleKeyFrame(
                    580,
                    KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(Game.PointerTime)),
                    new KeySpline(1.0, 0.0, 0.0, 0.0)
                    )
                );
                Storyboard.SetTargetName(keyFramehorizontal, hPointer.Name);
                Storyboard.SetTargetProperty(keyFramehorizontal, new PropertyPath(Canvas.LeftProperty));
                sBoardHorizontal.Children.Add(keyFramehorizontal);
            }
            sBoardHorizontal.RepeatBehavior = RepeatBehavior.Forever;
            sBoardHorizontal.AutoReverse = true;
        }

        private void ConfigureVAnimation()
        {
            sBoardVertical = new Storyboard();
            if (Game.CurrentLevel < 10 )
            {
                anim2 = new DoubleAnimation(90, 480, TimeSpan.FromMilliseconds(Game.PointerTime));
                Storyboard.SetTargetName(anim2, vPointer.Name);
                Storyboard.SetTargetProperty(anim2, new PropertyPath(Canvas.TopProperty));
                sBoardVertical.Children.Add(anim2);
            }
            else
            {
                keyFramevertical = new DoubleAnimationUsingKeyFrames();
                keyFramevertical.Duration = TimeSpan.FromMilliseconds(Game.PointerTime);
                keyFramevertical.KeyFrames.Add(
                new SplineDoubleKeyFrame(
                    480,
                    KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(Game.PointerTime)),
                    new KeySpline(1.0, 0.0, 0.0, 0.0)
                    )
                );
                
                Storyboard.SetTargetName(keyFramevertical, vPointer.Name);
                Storyboard.SetTargetProperty(keyFramevertical, new PropertyPath(Canvas.TopProperty));
                sBoardVertical.Children.Add(keyFramevertical);
            }
            sBoardVertical.RepeatBehavior = RepeatBehavior.Forever;
            sBoardVertical.AutoReverse = true;
        }

        private void ConfigureDartAnimation()
        {
            sBoardDart = new Storyboard();
            dartAnimationHorizontal = new DoubleAnimation(Canvas.GetLeft(dartImage), Game.HittingPoint.X, TimeSpan.FromMilliseconds(300));
            Storyboard.SetTargetName(dartAnimationHorizontal, dartImage.Name);
            Storyboard.SetTargetProperty(dartAnimationHorizontal, new PropertyPath(Canvas.LeftProperty));


            dartAnimationVertical = new DoubleAnimation(Canvas.GetTop(dartImage), Game.HittingPoint.Y, TimeSpan.FromMilliseconds(300));
            Storyboard.SetTargetName(dartAnimationVertical, dartImage.Name);
            Storyboard.SetTargetProperty(dartAnimationVertical, new PropertyPath(Canvas.TopProperty));

            sBoardDart.Children.Add(dartAnimationHorizontal);
            sBoardDart.Children.Add(dartAnimationVertical);
            sBoardDart.Completed += new EventHandler(DartAnimationCompleted);
        }

        private void ConfigureScoreAnimation()
        {
           
            scoreTxtBlock.Text = Game.HittingScore.ToString();
            scoreTxtBlock.Visibility = Visibility.Visible;
            sScoreAnim = new Storyboard();
            scoreAnimation = new DoubleAnimation(400, 200, TimeSpan.FromMilliseconds(600));            
            Storyboard.SetTargetName(scoreAnimation, scoreTxtBlock.Name);
            Storyboard.SetTargetProperty(scoreAnimation, new PropertyPath(Canvas.TopProperty));

            scoreAnimation2 = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(600));
            Storyboard.SetTargetName(scoreAnimation2, scoreTxtBlock.Name);
            Storyboard.SetTargetProperty(scoreAnimation2, new PropertyPath(TextBlock.OpacityProperty));

            sScoreAnim.Children.Add(scoreAnimation);
            sScoreAnim.Children.Add(scoreAnimation2);
            sScoreAnim.Completed += new EventHandler(sScoreAnim_Completed);
        
        }

       
        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sBoardHorizontal.GetIsPaused(mainGrid) == false && sBoardVertical.GetIsPaused(mainGrid)==true)
            {
                sBoardHorizontal.Pause(mainGrid);
                sBoardVertical.Resume(mainGrid);
            }

            else if (sBoardHorizontal.GetIsPaused(mainGrid) == true && sBoardVertical.GetIsPaused(mainGrid) == false)
            {
                sBoardVertical.Pause(mainGrid);
                Game.ThrowDart(Convert.ToInt32(Canvas.GetLeft(hPointer)), Convert.ToInt32(Canvas.GetTop(vPointer)));
                ConfigureDartAnimation();
                sBoardDart.Begin(mainGrid, true);
                
            }
           

        }

        private void DartAnimationCompleted(object sender, EventArgs args)
        {            
            WaitAfterDartAnimationTimer.Start();            
            
        }

        void sScoreAnim_Completed(object sender, EventArgs e)
        {
            scoreTxtBlock.Visibility = Visibility.Hidden;
            sScoreAnim.Stop(mainGrid);
            WaitAfterHittingScoreDisplay.Start();
        }
        
        private void WaitAfterDartAnimationTimer_Tick(object sender, EventArgs e)
        {
            WaitAfterDartAnimationTimer.Stop();
            ConfigureScoreAnimation();
            sScoreAnim.Begin(mainGrid,true);
           
           
            if (Game.ChanceCount == 1)
                lblScore1.Content = Game.HittingScore;

            else if (Game.ChanceCount == 2)
                lblScore2.Content = Game.HittingScore;

            else if (Game.ChanceCount == 3)
                lblScore3.Content = Game.HittingScore;

            lblTotalScore.Content = Game.LevelScore;
              
        }

        void WaitAfterScoreAnimation_Tick(object sender, EventArgs e)
        {
            WaitAfterHittingScoreDisplay.Start();      


        }

        private void WaitAfterHittingScoreDisplay_Tick(object sender, EventArgs e)
        {
                bool levelCleared = false;
                bool levelOver = false;
                sBoardDart.Stop(mainGrid);
                sBoardHorizontal.Resume(mainGrid);
                WaitAfterHittingScoreDisplay.Stop();
                levelOver=Game.NextChance(ref levelCleared);
                if (levelOver)
                {
                    sBoardHorizontal.Pause(mainGrid);                    
                    if (levelCleared)
                        Game.RaiseMessageBoxEvent("Congratulations, you have cleared this level !!");
                    //Xceed.Wpf.Toolkit.MessageBox.Show("Congratulations, you have cleared this level !!", "GoDarting Information", MessageBoxButton.OK,msgStyle);
                    else
                        Game.RaiseMessageBoxEvent("Sorry, you have failed this level !!");
                        //Xceed.Wpf.Toolkit.MessageBox.Show("Sorry, you have failed this level !!", "GoDarting Information", MessageBoxButton.OK, msgStyle);

                    if(Game.LevelCleared)
                        ChangeUI.RemoveScreen(ScreenName.PlayingScreen,ScreenName.LevelScreen);
                    else
                        ChangeUI.RemoveScreen(ScreenName.PlayingScreen, ScreenName.FinalScreen);
                }
        }
        
    }
}
