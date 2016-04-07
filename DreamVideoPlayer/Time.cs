using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace DreamMusicPlayer
{
    class Time
    {
        public void DefineTimeDefault(System.Windows.Forms.Label label1) {
            int s = 0;
            int heure = s / 60 / 60 % 24;
            int minute = s / 60 % 60;
            int seconde = s % 60;
            label1.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
        }
     
        
        
        public void TimeTick(System.Windows.Forms.Label label1, int tmp, System.Windows.Forms.TrackBar trackBar2, WMPLib.WindowsMediaPlayer myplayer, Timer timer1)
        {
            if (myplayer.playState.ToString().Contains("Playing"))
            {
               
                
                int s = ((int)myplayer.controls.currentPosition);
                tmp = (int)myplayer.currentMedia.duration;
                int heure = s / 60 / 60 % 24;
                int minute = s / 60 % 60;
                int seconde = s % 60;
                label1.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
                
            }
            else if (myplayer.playState.ToString().Contains("Stopped") || myplayer.playState.ToString().Contains("Finished"))
            {
                timer1.Stop();
                int s = 0;
                int heure = s / 60 / 60 % 24;
                int minute = s / 60 % 60;
                int seconde = s % 60;
                label1.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
                trackBar2.Value = 0;
            }
        }
           
    }
}
