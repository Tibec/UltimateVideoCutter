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

        private int _noOfErrorsOnScreen = 0;
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
            
            /* 
             * Message to emit
             */
            System.Diagnostics.Debug.WriteLine("In selection");
            //send a message to the view controler to set the selected index
            ListIndex listIndex = new ListIndex();
            listIndex.setValue(cutList.SelectedIndex);
            Messenger.Default.Send(listIndex);

        }


        private void Save_Error(object sender, ValidationErrorEventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("In save Error");

            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;

            if (_noOfErrorsOnScreen == 0)
            {
                bool toSent = true;
                NotificationMessage newMessage = new NotificationMessage(toSent, "ExportEnabling");
                Messenger.Default.Send(newMessage);

            }
            else
            {
                bool toSent = false;
                NotificationMessage newMessage = new NotificationMessage(toSent, "ExportEnabling");
                Messenger.Default.Send(newMessage);
            }

        }

        //ma,age the select even if we select a text box
        protected void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            item.IsSelected = true;

            //and send the new selection
           
        }

    }
}
