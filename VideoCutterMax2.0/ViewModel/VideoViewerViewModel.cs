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
using VideoCutterMax2.View; //to get the VideoViewer.currentTime

namespace VideoCutterMax2.ViewModel
{
    class VideoViewerViewModel : ViewModelBase
    {
        //selected Row
        public int selectedRow = -1;
        private void SetSelectedRow(ListIndex li)
        {
            selectedRow = li.getValue();
            BeginEnable = true;
            EndEnable = true;
        }
        private void ResetSelectedRow(MessengBool b)
        {
            if (b.DisableButton)
            {
                selectedRow = -1;
                BeginEnable = false;
                EndEnable = false;
            }
            
        }

        // mainVideo.Position , set with MediaElementExtension and the attached property
        

        //constructeur
        public VideoViewerViewModel()
        {
            Cuts = CutDatabase.GetCutDatabase();
            if (Cuts.VideoPath != null){
                CurrentVideo = new Video(Cuts.VideoPath,"CurrentVideo");
                IsVideoLoaded = true;
            }
            
           

            AddBeginCommand = new RelayCommand(() => AddBegin());
            AddEndCommand = new RelayCommand(() => AddEnd());

            Messenger.Default.Register<ListIndex>(this, SetSelectedRow);
            Messenger.Default.Register<MessengBool>(this, ResetSelectedRow);
            Messenger.Default.Register<Video>(this, ResetVideo);
            

        }


        /* 
         * Variables
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
        private Boolean _beginEnable = false;
        public Boolean BeginEnable
        {
            get { return _beginEnable; }
            set { Set("BeginEnable", ref _beginEnable, value); }
        }
        private Boolean _endEnable = false;
        public Boolean EndEnable
        {
            get { return _endEnable; }
            set { Set("EndEnable", ref _endEnable, value); }
        }
        private Boolean _isVideoLoaded = false;
        public Boolean IsVideoLoaded
        {
            get { return _isVideoLoaded; }
            set { Set("IsVideoLoaded", ref _isVideoLoaded, value); }
        }

        /*
         * Functions
         */
        private void ResetVideo(Video v)
        {
            
            Cuts.VideoPath = v.Uri;
            CurrentVideo = v;
            Cuts.ResetCutsDatabase();
            IsVideoLoaded = true;
        }

        // relay command
        public RelayCommand AddBeginCommand { get; set; }
        private void AddBegin()
        {
            if(selectedRow != -1 && selectedRow < Cuts.GetCollection().Count())
            {
                //needs to be improve (the static public currentTime)
                Cuts.GetCollection().ElementAt<Cut>(selectedRow).Begin = new TimeSpan(0,VideoViewer.currentTime.Hours, VideoViewer.currentTime.Minutes, VideoViewer.currentTime.Seconds);
                
            }
            
        }

        public RelayCommand AddEndCommand { get; set; }
        private void AddEnd()
        {
            if (selectedRow != -1 && selectedRow < Cuts.GetCollection().Count())
            {
                Cuts.GetCollection().ElementAt<Cut>(selectedRow).End = new TimeSpan(0, VideoViewer.currentTime.Hours, VideoViewer.currentTime.Minutes, VideoViewer.currentTime.Seconds);
                
            }
        }


        
        

    }
}
