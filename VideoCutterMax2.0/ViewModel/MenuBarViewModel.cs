using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace VideoCutterMax2.ViewModel
{
    class MenuBarViewModel : ViewModelBase
    {
        public MenuBarViewModel()
        {
            OpenFileDialog = new RelayCommand(() => btnOpenFile_Click());
        }




        public RelayCommand OpenFileDialog { get; set; }
        private void btnOpenFile_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
                System.Diagnostics.Debug.WriteLine(openFileDialog.FileName); 
        }
    }
}