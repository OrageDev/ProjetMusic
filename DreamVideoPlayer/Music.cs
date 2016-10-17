using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamMusicPlayer
{
    public class Music
    {


        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string artist;

        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private string nom_fichier;

        public string Nom_fichier
        {
            get { return nom_fichier; }
            set { nom_fichier = value; }
        }

        private int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private double size;

        public double Size
        {
            get { return size; }
            set { size = value; }
        }




        public Music()
        {
            nom_fichier = "inconnu";
            title = "inconnu";
            path = "inconnu";
            artist = "inconnu";
            length = -1;
            size = -1;
        }

        public Music(string new_nom_fichier, string new_title, string new_path, string new_artist, int new_length, double new_size)
        {
            nom_fichier = new_nom_fichier;
            title = new_title;
            path = new_path;
            artist = new_artist;
            length = new_length;
            size = new_size;
        }



    }
}
