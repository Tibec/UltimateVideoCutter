using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VideoCutterMax2.Model;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;




namespace VideoCutterMax2.ViewModel
{
    class CutListViewModel : ViewModelBase
    {
        //selected Row
        public int selectedRow = -1;
        private void SetSelectedRow(ListIndex li){
            selectedRow = li.getValue();
            DelEnable = true;
        }
        private void ResetSelectedRow(){
            selectedRow = -1;
            DelEnable = false;
        }


        public CutListViewModel()
        {
            Cuts = CutDatabase.GetCutDatabase();
            CutsList = BindGetCollection();
            //command
            AddCommand = new RelayCommand(() => Add());
            DelCommand = new RelayCommand(() => Del());
            UpdateEnd = new RelayCommand(() => Update());
            ExportCommand = new RelayCommand(() => Export());
            Messenger.Default.Register<ListIndex>(this, SetSelectedRow);
            Messenger.Default.Register<NotificationMessage>(this, UpdateExport);
        }

        private CutDatabase _cuts;
        public CutDatabase Cuts
        {
            get { return _cuts; }
            set { Set("Cuts", ref _cuts, value); }
        }

        //bind boolean for the Delete button
        private Boolean _delEnable = false;
        public Boolean DelEnable
        {
            get { return _delEnable; }
            set { Set("DelEnable", ref _delEnable, value); }
        }
        //bind boolean for the export button
        private Boolean _isExportable = false;
        public Boolean IsExportable
        {
            get { return _isExportable; }
            set { Set("IsExportable", ref _isExportable, value); }
        }
        /*
         * For the moment it is a temporary fix ( because i can't bind the function BindGetCollection or the _cuts attribut)
         * 
         */
        private ObservableCollection<Cut> _cutsList;
        public ObservableCollection<Cut> CutsList
        {
            get { return _cutsList; }
            set { Set("CutsList", ref _cutsList, value); }
        }
        /*
         * Use to get the observable cut collection
         */
        public ObservableCollection<Cut> BindGetCollection()
        {
           // System.Diagnostics.Debug.WriteLine("Instance: " + Cuts.GetCollection().ElementAt(0).Name);
            return Cuts.GetCollection();
        }

        /*
         * Button command
         */
        public RelayCommand AddCommand { get; set; }
        private void Add()
        {
            //System.Diagnostics.Debug.WriteLine("Instance: " + Cuts.GetCollection().ElementAt(0).Name);
            Cuts.AddCut(new Cut { Name = "Sans_nom", Begin = TimeSpan.Zero, End = TimeSpan.Zero });
            if(Cuts.VideoPath != null)
            {
                IsExportable = true;
            }
            
        }
        public RelayCommand DelCommand { get; set; }
        private void Del()
        {
            
            if ((selectedRow != -1) && (selectedRow < Cuts.Count()))
            {
                Cuts.RemoveAt(selectedRow);
                MessengBool reset = new MessengBool();
                reset.DisableButton = true;
                Messenger.Default.Send(reset);
                ResetSelectedRow();
                if(Cuts.Count() == 0)
                {
                    IsExportable = false;
                }
       
            }
        }

        // ne fonctionne pas sur le TextChanged event
        public RelayCommand UpdateEnd { get; set; }
        private void Update()
        {
            
           if(Cuts.GetAt(selectedRow).End == TimeSpan.Zero)
            {
                Cuts.GetAt(selectedRow).End = Cuts.GetAt(selectedRow).Begin;
            }

        } 

        private void UpdateExport(NotificationMessage m)
        {
            if(m.Notification == "ExportEnabling")
            {
                if ((bool)m.Sender == true)
                {
                    IsExportable = true;
                }
                else
                {
                    IsExportable = false;
                }
            }
        }

        public RelayCommand ExportCommand { get; set; }
        private void Export()
        {
            if (!Cuts.IsEmpty())
            {

                // check all the cuts

               /* for (int i = 0; i < Cuts.Count(); i++)
                {
                    if(Cuts.GetAt(i).Begin. > Cuts.GetAt(i).End)
                    {

                    }


                } */


                    /* 
                     * Create the split command with ffmpeg
                     */



                    string videoName = Cuts.VideoPath.ToString().Split('/').Last().Split('.').First();


                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false
                };

                process.StartInfo = startInfo;
                process.Start();
                

                process.StandardInput.WriteLine("mkdir \"D:/Utilisateurs/Raphael/Vidéos/"+videoName+"\"");
                // System.Diagnostics.Debug.WriteLine("mkdir \"D:/Utilisateurs/Raphael/Vidéos/" + videoName + "\"");

                process.StandardInput.WriteLine("d:");
                process.StandardInput.WriteLine("mkdir \"D:/Utilisateurs/Raphael/AppData/Local/ffmpeg/" + videoName + "\"");
                process.StandardInput.WriteLine("cd D:/Program Files/ffmpeg-4.0-win64-static/bin");
                for (int i = 0; i<Cuts.Count() ; i++){
                    //System.Diagnostics.Debug.WriteLine("ffmpeg -ss " + Cuts.GetAt(i).Begin.ToString() + " -to " + Cuts.GetAt(i).End.ToString() + " -i " + Cuts.VideoPath.LocalPath + " -c copy " + " D:/Utilisateurs/Raphael/Vidéos/" + videoName + "/" + Cuts.GetAt(i).Name+".mp4");
                    // await Task.Delay(1000);
                    process.StandardInput.WriteLine("cls");
                    string command = "ffmpeg -ss " + Cuts.GetAt(i).Begin.ToString().Substring(0, 8) + " -to " + Cuts.GetAt(i).End.ToString().Substring(0, 8) + " -i " + Cuts.VideoPath.LocalPath.Replace('\\','/') + " -c copy " + " D:/Utilisateurs/Raphael/AppData/Local/ffmpeg/" + videoName + "/" + Cuts.GetAt(i).Name + ".mp4";
                    process.StandardInput.WriteLine(command);

                }
                   

            }
           
        }






    }
}
