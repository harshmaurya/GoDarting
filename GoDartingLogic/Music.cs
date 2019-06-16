using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace GoDartingLogic
{
    public class Music
    {
       
        WMPLib.WindowsMediaPlayer wplayer;
        Random rand;
        List<string> musicList;
        public void StartMusic()
        {
            rand = new Random();
            musicList = new List<string>
                        {
                            "Resources/music1.mp3",
                            "Resources/music2.mp3",
                            "Resources/music3.MID",
                            "Resources/music4.mid"
                        };
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = musicList[rand.Next(musicList.Count)];
            wplayer.settings.setMode("loop", true);
            wplayer.controls.play();
                       
        }

        public void PauseMusic()
        {
            wplayer.controls.pause();
        }

        public void ResumeMusic()
        {
            wplayer.controls.play();
        }
    }
}
