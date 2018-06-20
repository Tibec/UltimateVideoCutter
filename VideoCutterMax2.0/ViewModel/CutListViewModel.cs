﻿using GalaSoft.MvvmLight;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;
using System.Drawing;




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
            Cuts.AddCut(new Cut { Name = "Sans_nom", Begin = TimeSpan.Zero, End = TimeSpan.Zero , Thumbnail = new Miniature()});
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


          
                string resourcesFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Assets");
                string videoName = Cuts.VideoPath.ToString().Split('/').Last().Split('.').First();

                


                //creation of the folder
                var process_cmd = new System.Diagnostics.Process();
                var startInfo_cmd = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false
                };

                process_cmd.StartInfo = startInfo_cmd;
                process_cmd.Start();

                string video_path = Cuts.VideoPath.LocalPath.Replace('\\', '/').Substring(0, Cuts.VideoPath.LocalPath.Replace('\\', '/').LastIndexOf('/')+1);
                
                process_cmd.StandardInput.WriteLine("mkdir " + "\"" + video_path + videoName + "\"");

                process_cmd.WaitForExit(1000);
                process_cmd.Kill();




                for (int i = 0; i < Cuts.Count(); i++)
                {
                    //boucle d'export une itération par vidéo
                    var process = new System.Diagnostics.Process();
                    var startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                        Arguments = "-ss " + Cuts.GetAt(i).Begin.ToString().Substring(0, 8) + " -to " + Cuts.GetAt(i).End.ToString().Substring(0, 8) + " -i " + Cuts.VideoPath.LocalPath.Replace('\\', '/') + " -c copy " + video_path + videoName + "/" + Cuts.GetAt(i).Name + ".mp4",
                        FileName = resourcesFolderPath + "\\" + "ffmpeg.exe",
                        
                        RedirectStandardInput = false,
                        UseShellExecute = true
                    };

                    /*process.StartInfo = startInfo;
                    process.Start();*/
                    System.Diagnostics.Debug.WriteLine(resourcesFolderPath + "\\" + "ffmpeg.exe");
                    process = Process.Start(startInfo);
                    //System.Diagnostics.Debug.WriteLine("ffmpeg -ss " + Cuts.GetAt(i).Begin.ToString() + " -to " + Cuts.GetAt(i).End.ToString() + " -i " + Cuts.VideoPath.LocalPath + " -c copy " + " D:/Utilisateurs/Raphael/Vidéos/" + videoName + "/" + Cuts.GetAt(i).Name+".mp4");
                    // await Task.Delay(1000);
                   // process.StandardInput.WriteLine("cls");
                   // process.StandardInput.WriteLine("cd " +"\""+ resourcesFolderPath + "\"" );

                   // string command = "ffmpeg -ss " + Cuts.GetAt(i).Begin.ToString().Substring(0, 8) + " -to " + Cuts.GetAt(i).End.ToString().Substring(0, 8) + " -i " + Cuts.VideoPath.LocalPath.Replace('\\', '/') + " -c copy " + video_path + videoName + "/" + Cuts.GetAt(i).Name + ".mp4";
                   
                    process.WaitForExit();



                    //export thumbnail
                    var thumb_location = video_path + videoName + "/" + Cuts.GetAt(i).Name + ".png";
                    //creation de la thumbnail
                    var image = GenerateThumb(i);
                    image.Save(thumb_location);
                   
                }



                /*
                process.StandardInput.WriteLine("d:");
                process.StandardInput.WriteLine("mkdir \"D:/Utilisateurs/Raphael/AppData/Local/ffmpeg/" + videoName + "\"");
                process.StandardInput.WriteLine("cd D:/Program Files/ffmpeg-4.0-win64-static/bin");
                */
            }          
        }


        private Bitmap GenerateThumb(int selectedRow)
        {
            // chargement de la miniature : on doit parcourir les 2 databases et afficher les perso checked

            var DbCharOne = Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase;
            var DbCharTwo = Cuts.GetAt(selectedRow).Thumbnail.TeamTwo.DataBase;

            // chargement de la miniature : on doit parcourir les 2 databases et afficher les perso checked
            var temp_image = new Bitmap(Cuts.GetAt(selectedRow).Thumbnail.Background.OriginalString);


            Graphics g = Graphics.FromImage(temp_image);




            //multiplicateur de position
            int[] mult = { 1, 1 };

            for (int i = 0; i < DbCharOne.Count; i++)
            {
                if (DbCharOne[i].IsPlayed)
                {
                    //afficher la miniature a gauche

                    var char_picture = new Bitmap(DbCharOne[i].PictureUri.OriginalString);
                    System.Diagnostics.Debug.WriteLine(DbCharOne[i].PictureUri.OriginalString);

                    //mise a echelle
                    var Smaller_char = new Bitmap(char_picture, new System.Drawing.Size(char_picture.Width / 3, char_picture.Height / 3));
                    Smaller_char.MakeTransparent();


                    System.Diagnostics.Debug.WriteLine(Smaller_char.Width);
                    var rect = new Rectangle(100 * mult[0], 100 * mult[0], Smaller_char.Width, Smaller_char.Height);
                    mult[0]+=2;
                    //image.WritePixels(rect, Data.Scan0, Data.Stride,100000);
                    g.DrawImage(Smaller_char, rect);
                }
            }
            for (int i = 0; i < DbCharTwo.Count; i++)
            {
                if (DbCharTwo[i].IsPlayed)
                {
                    //afficher la miniature a gauche

                    var char_picture = new Bitmap(DbCharTwo[i].PictureUri.OriginalString);
                    System.Diagnostics.Debug.WriteLine(DbCharTwo[i].PictureUri.OriginalString);

                    //mise a echelle
                    var Smaller_char = new Bitmap(char_picture, new System.Drawing.Size(char_picture.Width / 3, char_picture.Height / 3));
                    Smaller_char.MakeTransparent();


                    System.Diagnostics.Debug.WriteLine(Smaller_char.Width);
                    var rect = new Rectangle(temp_image.Width - Smaller_char.Width - (100 * mult[1]), (100 * mult[1]), Smaller_char.Width, Smaller_char.Height);
                    mult[1]+=2;
                    //image.WritePixels(rect, Data.Scan0, Data.Stride,100000);
                    g.DrawImage(Smaller_char, rect);
                }
            }



            // conversion et envoie de la bitmap source
            //
           
            g.Dispose();

            return temp_image;
        }







    }
}
