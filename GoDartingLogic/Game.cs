using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GoDartingLogic.PlayerDetailServiceReference;
using System.Security.Cryptography;
using System.Threading;

namespace GoDartingLogic
{
    public delegate void showMessage(string message);
    public delegate void ShowProgressBar();
    public delegate void HideProgressBar();
    public delegate void AuthenticationComplete();
    public delegate void ScoreSubmited();
    public delegate void Top20PlayersFetched();
    public delegate void CheckUsername();
    public delegate void RegisterPlayer();
    public delegate void ToggleSound(bool sound);

    public static class Game
    {
        public static event showMessage showMessageEvent;
        public static event ShowProgressBar ShowProgressBarEvent;
        public static event HideProgressBar HideProgressBarEvent;
        public static event AuthenticationComplete AuthenticationCompleteEvent;
        public static event ScoreSubmited ScoreSubmitedEvent;
        public static event Top20PlayersFetched Top20PlayersFetchedEvent;
        public static event CheckUsername CheckUsernameEvent;
        public static event RegisterPlayer RegisterPlayerEvent;
        public static event ToggleSound ToggleSoundEvent;

        public static int CurrentLevel { get; set; }
        public static int ChanceCount { get; set; }
        public static int PointerTime { get; set; }
        public static int PointsToClearLevel { get; set; }
        public static int HittingScore { get; set; }
        public static int LevelScore { get; set; }
        public static int FinalScore { get; set; }
        public static Point DistanceFromDartCenter { get; set; }
        public static Point HittingPoint { get; set; }
        public static bool LevelCleared { get; set; }
        public static PlayerDetail CurrentPlayer { get; set; }
        public static List<PlayerDetail> Top20Players { get; set; }
        public static int PlayerRank { get; set; }
        public static int TotalPlayers { get; set; }
        public static bool UsernameAvailable { get; set; }
               
        static Game()
        {
            CurrentLevel = 1;
            ChanceCount = 1;
            PointerTime = 1000;
            PointsToClearLevel = 200;
            LevelScore = 0;
            LevelCleared = true;
            FinalScore = 0;
           
        }

        public static void ToggleSound(bool sound)
        {
            if (ToggleSoundEvent != null)
                ToggleSoundEvent(sound);
        
        }

        public static void ResetGame()
        {
            CurrentLevel = 1;
            ChanceCount = 1;
            PointerTime = 1000;
            PointsToClearLevel = 200;
            LevelScore = 0;
            LevelCleared = true;
            FinalScore = 0;
        }

        public static void AuthenticateUser(string userName, string Password)
        {

            
                if (ShowProgressBarEvent != null)
                    ShowProgressBarEvent();

                new Thread((ThreadStart)delegate
                {
                    try
                    {

                        PlayerDetailServiceReference.PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                        CurrentPlayer = client.AuthenticatePlayer(userName, Encrypt(Password));
                        if (CurrentPlayer != null)
                        {
                            PlayerRank = GetRank();
                            TotalPlayers = GetTotalPlayers();
                        }
                        if (AuthenticationCompleteEvent != null)
                            AuthenticationCompleteEvent();
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();

                    }
                    catch (System.ServiceModel.EndpointNotFoundException ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
                    }

                    catch (Exception ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent(ex.Message);
                    }
                    finally
                    {
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }


                }).Start();                      
            

            
            
        }


        public static int GetRank()
        {
           try
            {
                PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                return client.GetRank(CurrentPlayer);
            }

           catch (System.ServiceModel.EndpointNotFoundException ex)
           {
               if (showMessageEvent != null)
                   showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
               return -1;
           }
            catch (Exception ex)
            {
                if (showMessageEvent != null)
                    showMessageEvent(ex.Message);
                return -1;
            }
            
        }


        public static int GetTotalPlayers()
        {
            
            try
            {
                PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                return client.GetTotalPlayers();
            }

            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                if (showMessageEvent != null)
                    showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
                return -1;
            }

            catch (Exception ex)
            {
                if (showMessageEvent != null)
                    showMessageEvent(ex.Message);
                return -1;
            }

        }

        public static void GetTop20Players()
        {
            
              if (ShowProgressBarEvent != null)
                    ShowProgressBarEvent();

                new Thread((ThreadStart)delegate
                {
                    try
                    {

                        PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                        Top20Players = client.GetTop20Players();
                        if (Top20PlayersFetchedEvent != null)
                            Top20PlayersFetchedEvent();
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }

                    catch (System.ServiceModel.EndpointNotFoundException ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
                    }

                    catch (Exception ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent(ex.Message);
                    }
                    finally
                    {
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }
                }).Start();

                
           

        }

        public static void RegisterPlayer(PlayerDetail player)
        {

                if (ShowProgressBarEvent != null)
                    ShowProgressBarEvent();

                new Thread((ThreadStart)delegate
                {
                    try
                    {

                        PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                        if (!client.CheckUsername(player.PlayerUserName))
                        {
                            client.RegisterNewPlayer(player);
                            Game.CurrentPlayer = player;
                            PlayerRank = GetRank();
                            TotalPlayers = GetTotalPlayers();
                            if (RegisterPlayerEvent != null)
                                RegisterPlayerEvent();
                            if (HideProgressBarEvent != null)
                                HideProgressBarEvent();
                            if (showMessageEvent != null)
                                showMessageEvent("You have been registered successfully");
                        }
                        else
                        {
                            if (showMessageEvent != null)
                                showMessageEvent("Username is not available");

                        }
                    }

                    catch (System.ServiceModel.EndpointNotFoundException ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
                    }

                    catch (Exception ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent(ex.Message);
                    }
                    finally
                    {
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }
                }).Start();


        
        }


        public static void Checkusername(string username)
        {
           
           
                if (ShowProgressBarEvent != null)
                    ShowProgressBarEvent();

                new Thread((ThreadStart)delegate
                {
                    try
                    {
                        string msg;
                        PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                        if (!client.CheckUsername(username))
                        {
                            msg = "Username available";
                            UsernameAvailable = true;
                        }
                        else
                        {
                            msg = "Username is not available";
                            UsernameAvailable = false;
                        }

                        if (showMessageEvent != null)
                            showMessageEvent(msg);
                        if (CheckUsernameEvent != null)
                            CheckUsernameEvent();
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }

                    catch (System.ServiceModel.EndpointNotFoundException ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
                    }

                    catch (Exception ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent(ex.Message);
                    }
                    finally
                    {
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }

                }).Start();

        
            
        }


        public static string Encrypt(string str)
        {
            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();
            byte[] unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool NextChance(ref bool levelCleared)
        {
            bool levelOver = false;
            if (ChanceCount < 3)
            {
                ChanceCount++;
            }
            else
            {
                levelOver = true;
                if (LevelScore >= PointsToClearLevel)
                {
                    levelCleared = true;
                    LevelCleared = true;
                    SetNextLevel();
                }
                else
                {
                    levelCleared = false;
                    LevelCleared = false;
                    FinalScore += LevelScore;
                }
                
            }
            return levelOver;
        }

        private static void SetNextLevel()
        {
            CurrentLevel++;
            ChanceCount = 1;
            FinalScore += LevelScore;
            LevelScore = 0;
            if (CurrentLevel < 3)
            {
                PointerTime -= 100;
                PointsToClearLevel += 20;
            }
            else if (CurrentLevel>=3 &&CurrentLevel < 6)
            {
                PointerTime -= 100;
                PointsToClearLevel -= 10;
            }
            else if (CurrentLevel >= 6 && CurrentLevel < 9)
            {
                PointerTime -= 100;
                PointsToClearLevel = 100;
            }

            else if (CurrentLevel >= 9 && CurrentLevel < 10)
            {
                PointsToClearLevel -= 10;
            }
            else if(CurrentLevel>=10 && CurrentLevel<15)
            {
                PointsToClearLevel += 10;
            }
        }

        public static void ThrowDart(int xPos, int yPos)
        {
            DistanceFromDartCenter = new System.Drawing.Point
            {
                X = Convert.ToInt32(((GameGeometry.HorizontalBarCenter - Convert.ToInt32(xPos))) / GameGeometry.BarToDartRatio),
                Y = Convert.ToInt32(((GameGeometry.VerticalBarCenter - Convert.ToInt32(yPos))) / GameGeometry.BarToDartRatio)
            };

            HittingPoint = new System.Drawing.Point
            {
                X = Convert.ToInt32(GameGeometry.DartBoardCenter.X) - DistanceFromDartCenter.X,
                Y = Convert.ToInt32(GameGeometry.DartBoardCenter.Y) - DistanceFromDartCenter.Y
            };

            double tmpDistance = Math.Sqrt(DistanceFromDartCenter.X*DistanceFromDartCenter.X + DistanceFromDartCenter.Y*DistanceFromDartCenter.Y);
            HittingScore = Convert.ToInt32(100 - tmpDistance);
            if (HittingScore < 0)
            HittingScore = 0;
            LevelScore += HittingScore;            
        }

        public static void RaiseMessageBoxEvent(string message)
        {
            showMessageEvent(message);
        }

     

        public static void SubmitScore()
        {
                if (ShowProgressBarEvent != null)
                    ShowProgressBarEvent();

                new Thread((ThreadStart)delegate
                {
                    try
                    {
                        PlayerDetailServiceClient client = new PlayerDetailServiceClient();
                        client.SubmitScore(CurrentPlayer, FinalScore);
                        PlayerRank = GetRank();
                        TotalPlayers = GetTotalPlayers();
                        if (CurrentPlayer.HighestScore < FinalScore)
                            CurrentPlayer.HighestScore = FinalScore;
                        if (showMessageEvent != null)
                            showMessageEvent("Your score has been submitted successfully");
                        if (ScoreSubmitedEvent != null)
                            ScoreSubmitedEvent();
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }

                    catch (System.ServiceModel.EndpointNotFoundException ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent("Cannot connect to the server. Make sure you are connected to the internet");
                    }

                    catch (Exception ex)
                    {
                        if (showMessageEvent != null)
                            showMessageEvent(ex.Message);
                    }
                    finally
                    {
                        if (HideProgressBarEvent != null)
                            HideProgressBarEvent();
                    }

                }).Start();


            
        }
    }
}
