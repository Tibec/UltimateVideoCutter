using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VideoCutterMax2.Model;
using GalaSoft.MvvmLight.Messaging;

namespace VideoCutterMax2.Model
{
    class CutDatabase
    {
        public Uri VideoPath { get; set; }
        public Uri CharacterRessourcesPath;
        static private CutDatabase instance;
        private ObservableCollection<Cut> _cuts;
       

        private CutDatabase()
        {
            _cuts = new ObservableCollection<Cut>();
            string resourcesFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Assets");
            CharacterRessourcesPath = new Uri(resourcesFolderPath + "\\" + "Character_db");
            instance = null;
            Messenger.Default.Register<NotificationMessage>(this, ChangeFolderPath);

        }

        public static CutDatabase GetCutDatabase()
        {
            if(instance == null)
            {
                instance = new CutDatabase();
                string resourcesFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Assets");
                instance.CharacterRessourcesPath = new Uri(resourcesFolderPath + "\\" + "Character_db");

            }
            
            return instance;
        }

        public ObservableCollection<Cut> GetCollection()
        {
            return instance._cuts;
        }

        public void AddCut(Cut c)
        {
            instance._cuts.Add(c);
        }
        public int Count()
        {
            if (instance != null)
            {
                return instance._cuts.Count();
            }
            else
            {
                return 0;
            }
            
        }
        public void RemoveAt(int i)
        {
            instance._cuts.RemoveAt(i);
        }

        public void ResetCutsDatabase()
        {
            instance._cuts.Clear();
        }
        public Cut GetAt(int i)
        {
            return instance._cuts[i];
        }

        public bool IsEmpty()
        {
            if(instance._cuts.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveToXml()
        {

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(CutDatabase));
            using (var writer = new StreamWriter(@"e:\test.xml"))
            {
                serializer.Serialize(writer, instance);
            }
        }

        private void ChangeFolderPath(NotificationMessage m)
        {
            
            if (m.Notification == "newCharacterFolder")
            {
                CharacterRessourcesPath = new Uri((string)m.Sender);
                //detruit les anciennes miniature

                for(int i = 0; i<instance.Count(); i++)
                {
                    instance.GetAt(i).Thumbnail.TeamOne = new CharacterDataBase();
                    instance.GetAt(i).Thumbnail.TeamTwo = new CharacterDataBase();
                }
                
            }
        }
    }
}
