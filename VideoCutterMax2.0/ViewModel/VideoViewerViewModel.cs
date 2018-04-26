using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCutterMax2.Model;
using VideoCutterMax2.View;

namespace VideoCutterMax2.ViewModel
{
    class VideoViewerViewModel : ViewModelBase
    {
        //selected Row
        public int selectedRow = -1;
        private void SetSelectedRow(ListIndex li)
        {
            selectedRow = li.getValue();
        }
        private void ResetSelectedRow()
        {
            selectedRow = -1;
        }

        //constructeur
        public VideoViewerViewModel()
        {
            Cuts = CutDatabase.GetCutDatabase();

            CurrentVideo = new Video(new Uri(@"D:\Utilisateurs\Raphael\Vidéos\GoPro_6.mp4"), "Test");
           

            AddBeginCommand = new RelayCommand(() => AddBegin());
            AddEndCommand = new RelayCommand(() => AddEnd());

            Messenger.Default.Register<ListIndex>(this, SetSelectedRow);
           
        }


        /* 
         */

        private Video _currentVideo = null;
        public Video CurrentVideo
        {
            get{return _currentVideo;}
            set{Set("CurrentVideo", ref _currentVideo, value);}
        }
        private CutDatabase _cuts;
        public CutDatabase Cuts
        {
            get { return _cuts; }
            set { Set("Cuts", ref _cuts, value); }
        }



        public RelayCommand AddBeginCommand { get; set; }
        private void AddBegin()
        {
            if(selectedRow != -1 && selectedRow < Cuts.GetCollection().Count())
            {
                VideoViewer test = new VideoViewer();
                Cuts.GetCollection().ElementAt<Cut>(selectedRow).Begin = test.mainVideo.Position.ToString().Substring(0, 8);
            }
            
        }

        public RelayCommand AddEndCommand { get; set; }
        private void AddEnd()
        {
            Cuts.GetCollection().ElementAt<Cut>(selectedRow).End = "00.00.00";
        }


        
        

    }
}
