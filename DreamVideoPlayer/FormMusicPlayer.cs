using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;
using System.Threading;
using System.IO;
using System.Deployment.Application;
using System.Reflection;

namespace DreamMusicPlayer
{
    public partial class FormMusicPlayer : Form
    {

#if DEBUG

        //static public WMPLib.WindowsMediaPlayer myplayer = new WindowsMediaPlayer();
        //static public SoundPlayer player = new SoundPlayer();
        //Thread mythread;
        Player monPlayer;
        WindowsMediaPlayer m;
        Calculs c;
        string[] _fichiers, _cheminFichier;
        string _fileURL;
        static string _filestatURL;
        string _typeChoose;
        Boolean _MouseIsPressedInTrackBar2;
        Boolean _MouseIsEnterTheTrackBar2;
        double _tdd;
        int tmp;
        Time t;


        //###################################################################### CONSTRUCTEUR ######################################################################
        /// <summary>
        /// Constructeur FormMusicPlayer
        /// Permet a chaque instanciation d'initialiser les variable de classe.
        /// </summary>
        public FormMusicPlayer()
        {
            monPlayer = new Player();
            m = monPlayer.GetmonPlayermdr();
            m.MediaChange += new _WMPOCXEvents_MediaChangeEventHandler(FunctionMediachange);
            m.EndOfStream += new _WMPOCXEvents_EndOfStreamEventHandler(EndOfTrack);
            c = new Calculs();
            InitializeComponent();
            _MouseIsPressedInTrackBar2 = false;
            _MouseIsEnterTheTrackBar2 = false;
            timer1.Interval = 500;
            
        }

        //##########################################################################################################################################################

        //###################################################################### Méthode Interne Graphique a la classe ######################################################################
        
        static public string GetFileURL()
        {
            return _filestatURL;
        }

        public void setTmp(int newTmp)
        {
            this.tmp = newTmp;
        }

        public void setMouseIsPressed(Boolean newVal)
        {

        }
        /// <summary>
        /// Méthode btn_Play_Click.
        /// Permet lorsque le bouton Play est préssé de jouer la musique sélectionné dans la liste.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Play_Click(object sender, EventArgs e) {

            if (_fileURL != null) {
                t = new Time();
                string chooseLayer = c.CuttingPath(_fileURL);
                c.FileKnowingPlaying(monPlayer, _fileURL, trackBar1, _typeChoose, labelLength, labelTime, labelType, tmp, chooseLayer, timer1, this);
                
            }
            else { }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (_fileURL != null)
            {
                string chooseLayer = c.CuttingPath(_fileURL);
                c.FileKnowingStopping(monPlayer,label1, labelEtat,trackBar2, chooseLayer, timer1);
            }
            else { }
        }
        private void trackBar1_Scroll(object sender, EventArgs e) {

            monPlayer.DefineVolume(trackBar1);
        }

        private void button7_Click(object sender, EventArgs e) {

            listBox1.Items.Clear();
            _fichiers = null;
            _cheminFichier = null;
        }

        //###################################################################### Event/Handler a la classe ######################################################################




        /// <summary>
        /// Event trackBar2_MouseDown.
        /// Permet lorsque l'on appuis sur la trackbar, de stop le player, de stop le timer, et de définir La variable boolean (_MouseIsPressedInTrackBar) comme étant présser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            c.mouseDown(_MouseIsPressedInTrackBar2, timer1, m);
        }


        /// <summary>
        /// Event trackBar2_MouseUp.
        /// Permet lorsque l'on relache le click souris de la trackbar, de définir la variable boolean (_MouseIsPressedInTrackBar) comme étant relachée, la position du curseur de la trackbar dans une variable, 
        /// de définir la position du player grace a la variable, puis de redemarrer le timer ainsi que le player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            c.mouseUp(_MouseIsPressedInTrackBar2,_tdd, trackBar2,m,timer1,this);
        }


        /// <summary>
        /// Méthode FunctionMediachange
        /// </summary>
        /// <param name="item"></param>
        public void FunctionMediachange(object item)
        {
        }

        /// <summary>
        /// Méthode EndOfTrack (OVERIDE DE "_WMPOCXEvents_EndOfStreamEventHandler")
        /// Permet de jouer la music suivante lorsque la musique actuelle est terminée en indiquant au status du player "Finished".
        /// </summary>
        /// <param name="res"></param>
        public void EndOfTrack(int res)
        {
            monPlayer.chooseNext(listBox1, trackBar2, trackBar1);
        }

        /// <summary>
        /// Méthode trackBar1_Scroll
        /// Permet de modifier le son.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            monPlayer.DefineVolume(trackBar1);
        }

        //#####################################################################################################################################################################

        //###################################################################### Méthode a la classe ##########################################################################


        /// <summary>
        /// Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int v = int.Parse(listBox1.Items.Count.ToString());
            if (v > 0)
            {
                _fileURL = _cheminFichier[listBox1.SelectedIndex];
                _filestatURL = _fileURL;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (_MouseIsPressedInTrackBar2 == true && _MouseIsEnterTheTrackBar2 == true)
            {
                //myplayer.controls.currentPosition = (trackBar2.Value  / (100 * tmp));
                //label5.Text = myplayer.controls.currentPosition.ToString();
                //label6.Text = _tdd.ToString();
                // SendtoAppend("trackBar2 Value = "+trackBar2.Value+", tmp = "+tmp);
            }
        }

        private void trackBar2_MouseEnter(object sender, EventArgs e)
        {
            _MouseIsEnterTheTrackBar2 = true;
        }

        private void trackBar2_MouseLeave(object sender, EventArgs e)
        {
            _MouseIsEnterTheTrackBar2 = false;
        }
        
        private void fileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Assembly assem = typeof(FormMusicPlayer).Assembly ;
            //AssemblyName assemName = assem.GetName();
            //Version ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            MessageBox.Show("It's Music player of mp3 and wav files.\nProgram created by Clyde Biddle (Orage)\nVersion: 1.0.2.9" , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void FormMusicPlayer_Load(object sender, EventArgs e)
        {

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            
           monPlayer.chooseNext(listBox1,trackBar2, trackBar1);
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            
            monPlayer.choosePrevious(listBox1,trackBar2,trackBar1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            labelEtat.Text = monPlayer.GetStatus();
            if (monPlayer.GetStatus().ToString() == "Stopped")
            {
                int v = listBox1.SelectedIndex;
                if (v + 1 < listBox1.Items.Count)
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    trackBar2.Value = 0;
                    string chooseLayer = c.CuttingPath(_fileURL);
                    btn_stop_Click(sender, e);
                    btn_Play_Click(sender, e);
                }
                else {}
            }
            else {}
            
            monPlayer.setTime(tmp, labelTime, this);

            t.TimeTick(label1,tmp, trackBar2, m, timer1);

            if(m.controls.currentPosition != 0 && tmp !=0)
            {
                int moi = (int)m.controls.currentPosition * 100;
                trackBar2.Value = moi / tmp;
            }
            else
            {
            }
            

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\n");
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            //if(_MouseIsPressedInTrackBar2 == true)
            //{
            //    myplayer.controls.currentPosition = myplayer.controls.currentPosition;
            //}

        }

        private int SetRealChangeTrackbar()
        {
             
            return Convert.ToInt32(_tdd);
        }

        private void labelEtat_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\Jeux\Osu!\Songs";
            openFileDialog1.Filter = "MP3 files/WAV files (*.mp3, *.mp4, *.wav) | *.mp3; *.mp4; *.wav";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _fichiers = openFileDialog1.SafeFileNames;
                _cheminFichier = openFileDialog1.FileNames;
                listBox1.Items.Clear();
                int compteur = 0;
                for (int i = 0; i < _fichiers.Length; i++)
                {
                    listBox1.Items.Add(_fichiers[i]);
                    compteur = i; 
                }
              
                //myplayer.settings.volume = trackBar1.Value;
                listBox1.SelectedIndex = 0;
            }
        }
#else
       static public WMPLib.WindowsMediaPlayer myplayer = new WindowsMediaPlayer();
        static public SoundPlayer player = new SoundPlayer();
        //Thread mythread;
        string[] _fichiers, _cheminFichier;
        string _fileURL;
        string _typeChoose;
        int tmp;
        Pen pen;
        public FormMusicPlayer()
        {
            

            myplayer.MediaChange += new _WMPOCXEvents_MediaChangeEventHandler(FunctionMediachange);
            myplayer.EndOfStream += new _WMPOCXEvents_EndOfStreamEventHandler(EndOfTrack);
            
            InitializeComponent();
            
            timer1.Interval = 100;
            pen = new Pen(Color.FromArgb(255, 0, 0, 0));
           
        }

        
        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen, 0, 100, 100, 100);
        }
        private void setTime()
        {
            //SendtoAppend("Méthode setTime appelée..");
            int s = (int)(myplayer.currentMedia.duration);
            tmp = s;
            int heure = s / 60 / 60 % 24;
            int minute = s / 60 % 60;
            int seconde = s % 60;
            labelTime.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
        }
        private void FunctionMediachange(object item) {
            labelEtat.Text = myplayer.status.ToString();
            if (myplayer.status.ToString() == "Finished") {
                chooseNext();
            }
            setTime();
        }

        private void EndOfTrack(int res)
        {
            chooseNext();
        }

        public void chooseNext()
        {
            //SendtoAppend("Méthode chooseNext appelée..");
            if (int.Parse(listBox1.Items.Count.ToString()) > (listBox1.SelectedIndex + 1))
            {
                
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                myplayer.controls.stop();
                trackBar2.Value = 0;
                PlayMusic(_fileURL);
            }

        }
        public void choosePrevious()
        {
            //SendtoAppend("Méthode choosePrevious appelée..");
            if (listBox1.SelectedIndex > 0)
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex -1;
                myplayer.controls.stop();
                trackBar2.Value = 0;
                PlayMusic(_fileURL);

            }

        }

        
        public void PlayMusic(string url)
        {
            //SendtoAppend("Méthode PlayMusic appelée..");
            myplayer.controls.stop();
            myplayer = new WindowsMediaPlayer();
            myplayer.MediaChange += new _WMPOCXEvents_MediaChangeEventHandler(FunctionMediachange);
            myplayer.EndOfStream += new _WMPOCXEvents_EndOfStreamEventHandler(EndOfTrack);
           
            myplayer.URL = url;
            myplayer.settings.volume = trackBar1.Value;
            myplayer.controls.play();
            setLength();
            setTypeLabel();
            setTime();

            //infoMusic = new Thread(() => InfoMusic(this));
            //infoMusic.Start();
            //mythread = new Thread(new ThreadStart(myThread));
            //mythread.Start();

        }

        public void PlaySound(string url)
        {
            player.SoundLocation = url;
        }

        private void SendtoAppend(string myline)
        {
            richTextBox1.AppendText("("+System.DateTime.Now.Hour.ToString("00") +":"+ System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00")+")" + "  "+ myline);
        }

        private void setTypeLabel()
        {
            //SendtoAppend("Méthode setTypeLabel appelée..");
            labelType.Text = _typeChoose;
        }

        private void setLength()
        {
            //SendtoAppend("Méthode setLength appelée..");
            FileInfo infoFichier = new FileInfo(_fileURL);
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


        private void btn_Play_Click(object sender, EventArgs e)
        {
            //SendtoAppend("Bouton Play préssé..");

            if (_fileURL != null)
            {
               
                string substringfileurl = _fileURL.Substring(_fileURL.LastIndexOf("\\"));
                string substringfileurl2 = substringfileurl.Substring(substringfileurl.LastIndexOf("."));
                string chooseLayer = substringfileurl2.Substring(1);

                if (chooseLayer.Contains("mp3"))
                {
                    _typeChoose = chooseLayer;
                   
                    PlayMusic(_fileURL);
                    timer1.Start();

                    
                    

                }
                else if (chooseLayer.Contains("wav"))
                {
                    PlaySound(_fileURL);
                    player.Play();
                }
               
            }
            else{}
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int v = int.Parse(listBox1.Items.Count.ToString());
            if (v > 0)
            {
                _fileURL = _cheminFichier[listBox1.SelectedIndex];
            }
        }

        

        private void btn_stop_Click(object sender, EventArgs e)
        {
            //SendtoAppend("Bouton Stop préssé..");
            

            if (_fileURL != null)

            {
                
                string substringfileurl = _fileURL.Substring(_fileURL.LastIndexOf("\\"));
                string substringfileurl2 = substringfileurl.Substring(substringfileurl.LastIndexOf("."));
                string chooseLayer = substringfileurl2.Substring(1);
                if (chooseLayer.Contains("mp3"))
                {
                    myplayer.controls.stop();
                    timer1.Stop();
                    label1.Text = "0";
                    myplayer.MediaChange += new _WMPOCXEvents_MediaChangeEventHandler(FunctionMediachange);
                    myplayer.EndOfStream += new _WMPOCXEvents_EndOfStreamEventHandler(EndOfTrack);
                    labelEtat.Text = "Stopped";
                    trackBar2.Value = 0;
                    int s = 0;
                    int heure = s / 60 / 60 % 24;
                    int minute = s / 60 % 60;
                    int seconde = s % 60;
                    label1.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
                }
                else if (chooseLayer.Contains("wav"))
                {
                    player.Stop();
                }
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            myplayer.settings.volume = trackBar1.Value;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //SendtoAppend("Bouton Clear List préssé..");
            listBox1.Items.Clear();
            _fichiers = null;
            _cheminFichier = null;
        }

        private void fileOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Assembly assem = typeof(FormMusicPlayer).Assembly ;
            //AssemblyName assemName = assem.GetName();
            //Version ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            MessageBox.Show("It's Music player of mp3 and wav files.\nProgram created by Clyde Biddle (Orage)\nVersion: 1.0.2.9" , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void FormMusicPlayer_Load(object sender, EventArgs e)
        {

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            //SendtoAppend("Bouton suivant préssé..");
            chooseNext();
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            //SendtoAppend("Bouton précédent préssé..");
            choosePrevious();
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myplayer.playState.ToString().Contains("Playing"))
            {
                    trackBar2.Value = ((int)myplayer.controls.currentPosition * 100 / tmp);

                    int s = ((int)myplayer.controls.currentPosition);

                    int heure = s / 60 / 60 % 24;
                    int minute = s / 60 % 60;
                    int seconde = s % 60;
                    label1.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
            }
            else if(myplayer.playState.ToString().Contains("Stopped") || myplayer.playState.ToString().Contains("Finished"))
            {
                timer1.Stop();
                int s = 0;
                int heure = s / 60 / 60 % 24;
                int minute = s / 60 % 60;
                int seconde = s % 60;
                label1.Text = Convert.ToString(heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + seconde.ToString().PadLeft(2, '0'));
               
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\n");
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }



        //private void trackBar2_ValueChanged(object sender, EventArgs e)
        //{
        //    myplayer.controls.currentPosition = trackBar2.Value / 100 * tmp;
        //}

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SendtoAppend("Méthode OuvertureFichier appelée..");
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\Jeux\Osu!\Songs";
            openFileDialog1.Filter = "MP3 files/WAV files (*.mp3, *.wav) | *.mp3; *.wav";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _fichiers = openFileDialog1.SafeFileNames;
                _cheminFichier = openFileDialog1.FileNames;
                listBox1.Items.Clear();
                int compteur = 0;
                for (int i = 0; i < _fichiers.Length; i++)
                {
                    listBox1.Items.Add(_fichiers[i]);
                    compteur = i; 
                }
                //SendtoAppend(compteur.ToString() + " de fichier mp3 ajoutés..."); 
                myplayer.settings.volume = trackBar1.Value;
                listBox1.SelectedIndex = 0;
            }
        }

#endif

    }

}
