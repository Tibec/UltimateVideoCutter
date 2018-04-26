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




namespace VideoCutterMax2.ViewModel
{
    class CutListViewModel : ViewModelBase
    {
        //selected Row
        public int selectedRow = -1;
        private void SetSelectedRow(ListIndex li){selectedRow = li.getValue();}
        private void ResetSelectedRow(){selectedRow = -1;}


        public CutListViewModel()
        {
            Cuts = CutDatabase.GetCutDatabase();
            Cuts.AddCut(new Cut() { Name = "Test", Begin = "10.00.25", End = "20.15.05" });
            Cuts.AddCut(new Cut() { Name = "Test2", Begin = "30.58.00", End = "40.36.28" });
            CutsList = BindGetCollection();
            //command
            AddCommand = new RelayCommand(() => Add());
            DelCommand = new RelayCommand(() => Del());
            Messenger.Default.Register<ListIndex>(this, SetSelectedRow);
        }

        private CutDatabase _cuts;
        public CutDatabase Cuts
        {
            get { return _cuts; }
            set { Set("Cuts", ref _cuts, value); }
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
            Cuts.AddCut(new Cut { Name = "Test"+ (Cuts.Count()+1).ToString(), Begin = "47.15.00", End = "65.32.20" });
        }
        public RelayCommand DelCommand { get; set; }
        private void Del()
        {
            
            if ((selectedRow != -1) && (selectedRow < Cuts.Count()))
            {
                Cuts.RemoveAt(selectedRow);
                ResetSelectedRow();
            }

        }

      




    }
}
