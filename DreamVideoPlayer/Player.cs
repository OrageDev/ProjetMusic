using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;


namespace DreamMusicPlayer
{
    

    class Player
    {


        public WMPLib.WindowsMediaPlayer monPlayermdr;
         

        //###################################################################### CONSTRUCTEUR ######################################################################
        public Player()
        {
            monPlayermdr = new WindowsMediaPlayer();
        }



        public WindowsMediaPlayer GetmonPlayermdr()
        {
            return monPlayermdr;
        }
        //##########################################################################################################################################################

        //###################################################################### Méthode Interne Non-graphique a la classe ######################################################################

        /// <summary>
        /// Méthode PlayMusic.
        /// Permet de lire les fichiers MP3 et MP4.
        /// </summary>
        /// 
        /// <param name="music"></param>
        public void PlayMusic(Music music, System.Windows.Forms.TrackBar trackBar1)
        {

            if (this.GetStatus() == "Finished"){
                
                
                monPlayermdr.URL = music.Path;
                monPlayermdr.settings.volume = trackBar1.Value;
                monPlayermdr.controls.play();
            }
            else
            {
                //monPlayermdr.controls.stop();
                monPlayermdr.URL = music.Path;
                monPlayermdr.settings.volume = trackBar1.Value;
                monPlayermdr.controls.play();
            } 
            
        }
        public void StopMusic(System.Windows.Forms.Label label1, System.Windows.Forms.Label labelEtat, System.Windows.Forms.TrackBar trackBar2)
        {
            monPlayermdr.controls.stop();
            CultureInfo uiCulture1 = CultureInfo.CurrentUICulture;
            CultureInfo uiCulture2 = Thread.CurrentThread.CurrentUICulture;
            Console.WriteLine("The current UI culture is {0}", uiCulture1.Name);
            Console.WriteLine("The two CultureInfo objects are equal: {0}", uiCulture1 == uiCulture2);
            labelEtat.Text = "Arrêté";
            label1.Text = "0";
            Time defaut = new Time();
            defaut.DefineTimeDefault(label1);
            defaut = null;
            trackBar2.Value = 0;

        }

        /// <summary>
        /// Méthode setTime. 
        /// Permet de définir le position du curseur dans le temps.
        /// </summary>
        /// 
        public void setTime(int tmp, System.Windows.Forms.Label labelTime, FormMusicPlayer monPlayer)
        {
            
            int s = (int)(monPlayermdr.currentMedia.duration);
            tmp = s;
            monPlayer.setTmp(tmp);
            int heure = s / 60 / 60 % 24;
            int minute = s / 60 % 60;
            int seconde = s % 60;
            labelTime.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
        }


        /// <summary>
        /// Méthode setTypeLabel.
        /// Permet de définir le type de fichier selectionné.
        /// </summary>
        public void setTypeLabel(System.Windows.Forms.Label labelType, string _typeChoose)
        {
          
            labelType.Text = _typeChoose;
        }

        /// <summary>
        /// Méthode setLength
        /// Permet de définir la taille du fichier selectionné.
        /// </summary>
        /// 
        public void setLength(System.Windows.Forms.Label labelLength, Music music)
        {
        
            FileInfo infoFichier = new FileInfo(music.Path);
            if (6 < infoFichier.Length.ToString().Length)
            {
                float total = (float)infoFichier.Length / 1024 / 1024;
                labelLength.Text = total.ToString("0.00") + " Mb";
            }
            else if (3 < infoFichier.Length.ToString().Length)
            {
                float total = (float)infoFichier.Length / 1024;
                labelLength.Text = total.ToString("0.00") + " Kb";
            }
        }

        /// <summary>
        /// 
        /// Méthode chooseNext.
        /// Permet de choisir la prochaine fichier dans la liste et de la jouer.
        /// </summary>
        public void chooseNext(System.Windows.Forms.ListBox listBox1, System.Windows.Forms.TrackBar trackBar2, System.Windows.Forms.TrackBar trackBar1, List<Music> musicList)
        {
           
            if (int.Parse(listBox1.Items.Count.ToString()) > (listBox1.SelectedIndex + 1))
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                trackBar2.Value = 0;
               
                //string _fileURL = FormMusicPlayer.GetFileURL();
                PlayMusic(musicList[listBox1.SelectedIndex], trackBar1);
            }
        }

        /// <summary>
        /// Méthode choosePrevious.
        /// Permet de choisir le fichier précédent dans la liste et de la jouer.
        /// </summary>
        public void choosePrevious(System.Windows.Forms.ListBox listBox1, System.Windows.Forms.TrackBar trackBar2, System.Windows.Forms.TrackBar trackBar1, List<Music> musicList)
        {
            if (listBox1.SelectedIndex > 0)
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                monPlayermdr.controls.stop();
                trackBar2.Value = 0;
                //string _fileURL = FormMusicPlayer.GetFileURL();
                PlayMusic(musicList[listBox1.SelectedIndex], trackBar1);
            }
        }

        public void DefineVolume(System.Windows.Forms.TrackBar trackBar1)
        {
            monPlayermdr.settings.volume = trackBar1.Value;
        }

        public string GetStatus()
        {

            return monPlayermdr.status;
        }

        public int GetVolume()
        {
            return monPlayermdr.settings.volume;
        }
        //#####################################################################################################################################################################################

        //private void SendtoAppend(string myline, System.Windows.Forms.RichTextBox richTextBox1)
        //{

        //    richTextBox1.AppendText("(" + System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00") + ")" + "  " + myline);
        //}

        //###################################################################### Event/Handler a la classe ######################################################################



    }
}
