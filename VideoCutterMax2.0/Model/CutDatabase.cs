using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using VideoCutterMax2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace VideoCutterMax2.Model
{
    public class CutDatabase
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
                //si le fichier de sauvegarde existe on charge le JSON
                string saveFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Save");
                instance = new CutDatabase();
                System.Diagnostics.Debug.WriteLine(File.Exists(saveFolderPath + "\\instance_param.json"));
                System.Diagnostics.Debug.WriteLine(File.Exists(saveFolderPath + "\\db_save.json"));
                if (File.Exists(saveFolderPath + "\\instance_param.json") && File.Exists(saveFolderPath + "\\db_save.json"))
                {

                    System.Diagnostics.Debug.WriteLine("Lancement load");
                    
                    instance.LoadFromJSON(saveFolderPath);
                }
                else
                {
                    
                    string resourcesFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Assets");
                    instance.CharacterRessourcesPath = new Uri(resourcesFolderPath + "\\" + "Character_db");
                    System.Diagnostics.Debug.WriteLine("Pas load");
                }
                
                

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

        public void SaveToJSON()
        {

            string saveFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Save");

            string json_instance = JsonConvert.SerializeObject(instance, Formatting.Indented);
            string json_db = JsonConvert.SerializeObject(instance.GetCollection(), Formatting.Indented);
            System.IO.File.WriteAllText(saveFolderPath + "\\instance_param.json", json_instance);
            System.IO.File.WriteAllText(saveFolderPath + "\\db_save.json", json_db);

            System.Diagnostics.Debug.WriteLine(json_instance);
            

        }

        private void LoadFromJSON(string saveFolderPath)
        {
            if (File.Exists(saveFolderPath + "\\instance_param.json"))
            {
                string json_instance = File.ReadAllText(saveFolderPath + "\\instance_param.json");
                // initialise the instance parameter
                dynamic results = JsonConvert.DeserializeObject<dynamic>(json_instance);
                VideoPath = results.VideoPath;
                CharacterRessourcesPath = results.CharacterRessourcesPath;
            }

            if (File.Exists(saveFolderPath + "\\db_save.json"))
            {
                string json_instance = File.ReadAllText(saveFolderPath + "\\db_save.json");
                // initialise the instance parameter
                List<Cut> results = JsonConvert.DeserializeObject<List<Cut>>(json_instance);
                
                _cuts= new ObservableCollection<Cut>(results as List<Cut>);
                System.Diagnostics.Debug.Write(_cuts[0].Thumbnail.TeamOne.DataBase[0].Name);
                System.Diagnostics.Debug.WriteLine(_cuts[0].Thumbnail.TeamOne.DataBase[0].IsPlayed);
              /*  JObject o = JObject.Parse(json_instance);
                foreach (var item in o)
                {
                    var single = item.Value.ToString();
                    var nodeValues = JsonConvert.DeserializeObject<List<CharacterDataBase>>(single);
                    // here do what you want to do for each root elements
                    System.Diagnostics.Debug.WriteLine(nodeValues);
                } */



            }
            //fill the db
            
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
