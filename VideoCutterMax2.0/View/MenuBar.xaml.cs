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
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using VideoCutterMax2.Model;


namespace VideoCutterMax2.View
{
    /// <summary>
    /// Logique d'interaction pour MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        public MenuBar()
        {
            InitializeComponent();

        }


        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers vidéo (*.mp4)| *.mp4";
            if (openFileDialog.ShowDialog() == true)
            {

                MessengBool reset = new MessengBool();
                reset.DisableButton = true;
                Messenger.Default.Send(reset);
                Video video = new Video(new Uri(openFileDialog.FileName), "New Video");
                Messenger.Default.Send(video);
            }

        }

        private void btnChangeFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
              

                NotificationMessage newMessage = new NotificationMessage(dialog.FileName, "newCharacterFolder");
                Messenger.Default.Send(newMessage);
            }

        }

        private void EmptyCutList(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Cette action est irrémédiable. Voulez vous continuer?","Attention!", MessageBoxButton.OKCancel);
           
            switch (result)
            {
                case MessageBoxResult.OK:
                    CutDatabase.GetCutDatabase().ResetCutsDatabase();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }


        }

        private void SaveList(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Cela va supprimer l'ancienne sauvegarde! Voulez vous continuer?", "Attention!", MessageBoxButton.OKCancel);

            switch (result)
            {
                case MessageBoxResult.OK:
                    CutDatabase.GetCutDatabase().SaveToJSON();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }


        }
    }
}
