using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using VideoCutterMax2.Model;


namespace VideoCutterMax2.View
{
    /// <summary>
    /// Logique d'interaction pour CutList.xaml
    /// </summary>
    public partial class CutList : UserControl
    {
        public CutList()
        {
            InitializeComponent();
        }

        //view events
        

        private void DisableButton(object sender, RoutedEventArgs e)
           {
               listDelete.IsEnabled = false;
           }
        private void SelectionChanged(object sender, RoutedEventArgs e)
        {
            listDelete.IsEnabled = true;
            /* 
             * Message to emit
             */
            System.Diagnostics.Debug.WriteLine("In selection");
            //send a message to the view controler to set the selected index
            ListIndex listIndex = new ListIndex();
            listIndex.setValue(cutList.SelectedIndex);
            Messenger.Default.Send(listIndex);
            //send a message to the videoViewer to enable button
            //Messenger.Default.Send<int>(cutList.SelectedIndex);
        }


    }
}
