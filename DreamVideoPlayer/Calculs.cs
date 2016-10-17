using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DreamMusicPlayer
{
    class Calculs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MouseIsPressedInTrackBar2"></param>
        /// <param name="_tdd"></param>
        /// <param name="trackBar2"></param>
        /// <param name="m"></param>
        /// <param name="timer1"></param>
        /// <param name="maform"></param>
        public void mouseUp(bool _MouseIsPressedInTrackBar2, double _tdd, System.Windows.Forms.TrackBar trackBar2, WMPLib.WindowsMediaPlayer m, Timer timer1, FormMusicPlayer maform )
        {
            _MouseIsPressedInTrackBar2 = false;
            maform.setMouseIsPressed(false);
            _tdd = (((double)trackBar2.Value * m.currentMedia.duration) / 100);
            //trackBar2.LargeChange = SetRealChangeTrackbar();
            m.controls.currentPosition = _tdd;
            timer1.Start();
            m.controls.play();
        }


        /// <summary>
        /// Event trackBar2_MouseDown.
        /// Permet lorsque l'on appuis sur la trackbar, de stop le player, de stop le timer, et de définir La variable boolean (_MouseIsPressedInTrackBar) comme étant présser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mouseDown(bool _MouseIsPressedInTrackBar2, Timer timer1,WMPLib.WindowsMediaPlayer m) 
        {
            _MouseIsPressedInTrackBar2 = true;
            timer1.Stop();
            m.controls.pause();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monPlayer"></param>
        /// <param name="music"></param>
        /// <param name="trackBar1"></param>
        /// <param name="_typeChoose"></param>
        /// <param name="labelLength"></param>
        /// <param name="labelTime"></param>
        /// <param name="labelType"></param>
        /// <param name="tmp"></param>
        /// <param name="chooseLayer"></param>
        /// <param name="timer1"></param>
        /// <param name="maform"></param>
        public void FileKnowingPlaying(Player monPlayer, Music music, System.Windows.Forms.TrackBar trackBar1, string _typeChoose, System.Windows.Forms.Label labelLength, System.Windows.Forms.Label labelTime, System.Windows.Forms.Label labelType, int tmp, string chooseLayer, Timer timer1, FormMusicPlayer maform)
        {
            
            if (chooseLayer.ToLower().Contains("mp3") || chooseLayer.ToLower().Contains("mp4"))  {

                    
                    WMPLib.WindowsMediaPlayer m = monPlayer.GetmonPlayermdr();
                    
                    monPlayer.PlayMusic(music, trackBar1);
                    monPlayer.setLength(labelLength, music);
                    monPlayer.setTime(tmp, labelTime, maform);
                    monPlayer.setTypeLabel(labelType, _typeChoose);
                    timer1.Start();
                }
                else if (chooseLayer.Contains("wav")) {

                    //PlaySound(_fileURL);
                    //player.Play();
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monPlayer"></param>
        /// <param name="label1"></param>
        /// <param name="labelEtat"></param>
        /// <param name="trackBar2"></param>
        /// <param name="chooseLayer"></param>
        /// <param name="timer1"></param>
        public void FileKnowingStopping(Player monPlayer, System.Windows.Forms.Label label1, System.Windows.Forms.Label labelEtat, System.Windows.Forms.TrackBar trackBar2, string chooseLayer, Timer timer1)
        {

            if (chooseLayer.ToLower().Contains("mp3") || chooseLayer.ToLower().Contains("mp4"))
            {
                monPlayer.StopMusic(label1, labelEtat, trackBar2);
                timer1.Stop();
            }
            else if (chooseLayer.ToLower().Contains("wav"))
            {
                //player.Stop();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_fileURL"></param>
        /// <returns></returns>
        public string CuttingPath(string _fileURL)
        {
            string substringfileurl = _fileURL.Substring(_fileURL.LastIndexOf("\\"));
            string substringfileurl2 = substringfileurl.Substring(substringfileurl.LastIndexOf("."));
            string chooseLayer = substringfileurl2.Substring(1);
            return chooseLayer;
        }
    }
}
