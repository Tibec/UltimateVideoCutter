using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace VideoCutterMax2.Model
{
    class CharacterDataBase : ObservableObject
    {
        public List<Character> DataBase = new List<Character>();

        private String _teamName;
        public String TeamName
        {
            get { return _teamName; }
            set { Set("TeamName", ref _teamName, value); }
        }

        public CharacterDataBase()
        {

            var tempDB = CutDatabase.GetCutDatabase();
            /* Parcours du fichier de personnages */
            
            
            foreach (string png in Directory.GetFiles(tempDB.CharacterRessourcesPath.OriginalString, "*.png"))
            {
                 
                string name = png.Split('\\').Last();
                name = name.Split('(')[0];       
                DataBase.Add(new Character(new Uri(png),name));
                
            }
            TeamName = "Equipe";
        }

        public CharacterDataBase(string resourcesFolderPath)
        {


            /* Parcours du fichier de personnages */
            
            

            foreach (string png in Directory.GetFiles(resourcesFolderPath, "*.png"))
            {

                string name = png.Split('\\').Last();
                name = name.Split('(')[0];
                DataBase.Add(new Character(new Uri(png), name));

            }
            TeamName = "Equipe";
        }
    }
}
