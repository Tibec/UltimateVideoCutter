using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace VideoCutterMax2.Model
{
    class Miniature
    {
        public Uri Background { get; set; }
        public CharacterDataBase TeamOne { get; set; }
        public CharacterDataBase TeamTwo { get; set; }

        public Miniature()
        {

            string resourcesFolderPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Assets");
            resourcesFolderPath = resourcesFolderPath + "\\" + "Fond";
            Background = new Uri(resourcesFolderPath + "\\default.png");
            TeamOne = new CharacterDataBase();
            TeamTwo = new CharacterDataBase();
        }

    }
}
