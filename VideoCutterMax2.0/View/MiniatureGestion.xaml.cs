using GalaSoft.MvvmLight.Messaging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VideoCutterMax2.Model;


namespace VideoCutterMax2.View
{
    /// <summary>
    /// Logique d'interaction pour MiniatureGestion.xaml
    /// </summary>
    public partial class MiniatureGestion : UserControl
    {

        public WriteableBitmap image;
     

        public MiniatureGestion()
        {
            

            InitializeComponent();

            var resourcesFolderPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "Assets");
            resourcesFolderPath = resourcesFolderPath + "\\" + "Fond";

            Messenger.Default.Register<ImageSend>(this, ResetPreview);
            

            var pictureFolderPath = System.IO.Path.Combine(Directory.GetParent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName, "pictures");
            var refreshPicture = new BitmapImage(new Uri(pictureFolderPath + "\\reload.png"));

            RefreshButton.Source = refreshPicture;

            var temp_image = new BitmapImage(new Uri(resourcesFolderPath + "\\default.png"));
            image = new WriteableBitmap(temp_image);
          
           // Preview.Source = image;
           // Background.Source = image;
        }


        private void ResetPreview(ImageSend imgs)
        {

            

            image = imgs.image;
           
            Preview.Source = image;
           
        }
       

    }
}
