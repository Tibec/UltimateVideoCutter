using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VideoCutterMax2.Model;
using System.Drawing;


namespace VideoCutterMax2.ViewModel
{
    class MiniatureGestionViewModel : ViewModelBase
    {


        private CharacterDataBase _teamOne;
        public CharacterDataBase TeamOne
        {
            get { return _teamOne; }
            set { Set("TeamOne", ref _teamOne, value); }
        }
        private List<Character> _dbCharOne;
        public List<Character> DbCharOne
        {
            get { return _dbCharOne; }
            set { Set("DbCharOne", ref _dbCharOne, value); }
        }

        private CharacterDataBase _teamTwo;
        public CharacterDataBase TeamTwo
        {
            get { return _teamTwo; }
            set { Set("TeamTwo", ref _teamTwo, value); }
        }
        private List<Character> _dbCharTwo;
        public List<Character> DbCharTwo
        {
            get { return _dbCharTwo; }
            set { Set("DbCharTwo", ref _dbCharTwo, value); }
        }

        private CutDatabase _cuts;
        public CutDatabase Cuts
        {
            get { return _cuts; }
            set { Set("Cuts", ref _cuts, value); }
        }

        private bool _refreshbutton;
        public bool RefreshButton
        {
            get { return _refreshbutton; }
            set { Set("RefreshButton", ref _refreshbutton, value); }
        }

        public MiniatureGestionViewModel()
        {
            Cuts = CutDatabase.GetCutDatabase();
            TeamOne = null;
            TeamTwo = null;
            DbCharOne = null;
            DbCharTwo = null;
            RefreshButton = false;


            ResetPreviewCommand = new RelayCommand(() => ResetPreview());
            Messenger.Default.Register<MessengBool>(this, ResetSelectedRow);
            Messenger.Default.Register<ListIndex>(this, SetSelectedRow);
            Messenger.Default.Register<NotificationMessage>(this, ChangeBackGround);


        }
    
        public int selectedRow = -1;
        private void SetSelectedRow(ListIndex li)
        {
            selectedRow = li.getValue();
            RefreshButton = true;
            //chargement des databases
            if(selectedRow != -1)
            {
                TeamOne = Cuts.GetAt(selectedRow).Thumbnail.TeamOne;
                TeamTwo = Cuts.GetAt(selectedRow).Thumbnail.TeamTwo;
                DbCharOne = Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase;
                DbCharTwo = Cuts.GetAt(selectedRow).Thumbnail.TeamTwo.DataBase;
                ResetPreview();
            }
           
        }
        private void ResetSelectedRow(MessengBool b)
        {
            if (b.DisableButton)
            {

                System.Diagnostics.Debug.WriteLine("ResetMiniature boutton");
                selectedRow = -1;
                RefreshButton = false;
                DbCharOne = null;
                DbCharTwo = null;
            }
            
        }

        public RelayCommand ResetPreviewCommand { get; set; }
        /* private void ResetPreview()
         {
             // chargement de la miniature : on doit parcourir les 2 databases et afficher les perso checked
             var temp_image = new Bitmap(Cuts.GetAt(selectedRow).Thumbnail.Background.OriginalString);

             int sizeChar = 50;
             Graphics g = Graphics.FromImage(temp_image);
             Font f = new Font("Arial", sizeChar);
             SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
             PointF drawPoint = new PointF(480.0F - (TeamOne.TeamName.Length * sizeChar)/2 , temp_image.Height - 250);

             //write team name 1
             g.DrawString(TeamOne.TeamName, f, drawBrush, drawPoint) ;
             //write team name 2
             drawPoint.X = temp_image.Width - 480.0F -(TeamTwo.TeamName.Length * sizeChar)/2;
             drawPoint.Y = temp_image.Height - 250;
             g.DrawString(TeamTwo.TeamName, f, drawBrush, drawPoint);

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
                     var rect = new Rectangle(temp_image.Width - Smaller_char.Width - (100 * mult[1]),(100 * mult[1]), Smaller_char.Width, Smaller_char.Height);
                     mult[1]+=2;

                     g.DrawImage(Smaller_char, rect);
                 }
             }

              g.Dispose(); 
             //reset preview /!\ FUITE MEMOIRE /!\
             /*image_src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                       imgs.image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                       System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());*/

        //  imgs.image.Save(System.Reflection.Assembly.GetExecutingAssembly().Location + "temp.png");
        /* imgs.image.Save(@"C:\Users\Raphaël Meier\source\repos\VideoCutterMax2.0\VideoCutterMax2.0\Assets\temp.jpg");


        var temp = new BitmapImage();
        temp.BeginInit();
        temp.CacheOption = BitmapCacheOption.OnLoad;
        temp.UriSource = new Uri(@"C:\Users\Raphaël Meier\source\repos\VideoCutterMax2.0\VideoCutterMax2.0\Assets\temp.jpg");
        temp.EndInit();
        // sans transparence pas de fuite mémoire
         image = new WriteableBitmap(temp);
        if (File.Exists(@"C:\Users\Raphaël Meier\source\repos\VideoCutterMax2.0\VideoCutterMax2.0\Assets\temp.jpg"))
        {


            File.Delete(@"C:\Users\Raphaël Meier\source\repos\VideoCutterMax2.0\VideoCutterMax2.0\Assets\temp.jpg");
        }*/

        //envoie de la bitmap 
        /*
                    var imgs = new ImageSend(temp_image, mult);
                   Messenger.Default.Send(imgs);


                } 
                */



        private void ResetPreview()
        {
            // chargement de la miniature : on doit parcourir les 2 databases et afficher les perso checked
            var temp_image = new BitmapImage(Cuts.GetAt(selectedRow).Thumbnail.Background);
           
            
           
            
            var image = new WriteableBitmap(temp_image);

           

            //multiplicateur de position
            int[] mult = { 1, 1 };

            for (int i = 0; i < DbCharOne.Count; i++)
            {
                if (DbCharOne[i].IsPlayed)
                {
                    //afficher la miniature a gauche

                    var char_picture = new BitmapImage(DbCharOne[i].PictureUri);


                    //mise a echelle
                    var Smaller_char = new TransformedBitmap(char_picture, new ScaleTransform(0.3, 0.3));
                    
                    
                    //recupère l'image bitmap en pixel
                    int stride = (int)Smaller_char.PixelWidth * (Smaller_char.Format.BitsPerPixel / 8);
                    

                    byte[] pixels = new byte[(int)Smaller_char.PixelHeight * stride];
                    System.Diagnostics.Debug.WriteLine(pixels.Length);


                    Smaller_char.CopyPixels( pixels, stride, 0);
                    
                    // défini la zone de collage
                    var rect = new Int32Rect(100 * mult[0], 100 * mult[0], Smaller_char.PixelWidth, Smaller_char.PixelHeight);
                    mult[0]++;
                    image.WritePixels(rect, pixels, stride, 0);
                    
                }
            }
            for (int i = 0; i < DbCharTwo.Count; i++)
            {
                if (DbCharTwo[i].IsPlayed)
                {
                    //afficher la miniature a droite
                    //afficher la miniature a gauche

                    var char_picture = new BitmapImage(DbCharTwo[i].PictureUri);
                    //mise a echelle
                    var Smaller_char = new TransformedBitmap(char_picture, new ScaleTransform(-0.3, 0.3));
                    //recupère l'image bitmap en pixel
                    int stride = (int)Smaller_char.PixelWidth * (Smaller_char.Format.BitsPerPixel / 8);
                    byte[] pixels = new byte[(int)Smaller_char.PixelHeight * stride];
                    Smaller_char.CopyPixels(pixels, stride, 0);
                    // défini la zone de collage
                    var rect = new Int32Rect(image.PixelWidth - Smaller_char.PixelWidth - (100 * mult[1]), 100 * mult[1], Smaller_char.PixelWidth, Smaller_char.PixelHeight);
                    mult[1]++;
                    image.WritePixels(rect, pixels, stride, 0);
                }
            }


            //envoie de la photo pour update
            var imgs = new ImageSend(image, mult);
            Messenger.Default.Send(imgs);
        } 

        private void ChangeBackGround(NotificationMessage nm)
        {
            if(nm.Notification == "newBackGround")
            {
                Cuts.GetAt(selectedRow).Thumbnail.Background = new Uri((string)nm.Sender);
            }
        }




    }
}
