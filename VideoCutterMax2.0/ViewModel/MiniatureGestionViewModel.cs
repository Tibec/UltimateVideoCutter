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
using Microsoft.Win32;
using VideoCutterMax2.Model;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace VideoCutterMax2.ViewModel
{
    class MiniatureGestionViewModel : ViewModelBase
    {


        // MemoryLeak correctif
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);



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

        private ImageSource _background;
        public ImageSource Background
        {
            get { return _background; }
            set { Set("Background", ref _background, value); }
        }

        private bool _refreshbutton;
        public bool RefreshButton
        {
            get { return _refreshbutton; }
            set { Set("RefreshButton", ref _refreshbutton, value); }
        }
        private bool _backgroundbutton;
        public bool BackgroundButton
        {
            get { return _backgroundbutton; }
            set { Set("BackgroundButton", ref _backgroundbutton, value); }
        }

        public MiniatureGestionViewModel()
        {
            Cuts = CutDatabase.GetCutDatabase();
            TeamOne = null;
            TeamTwo = null;
            DbCharOne = null;
            DbCharTwo = null;
            RefreshButton = false;
            BackgroundButton = false;

            ResetPreviewCommand = new RelayCommand(() => ResetPreview());
            btnOpenBack_Click = new RelayCommand(() => OpenB());
            Messenger.Default.Register<MessengBool>(this, ResetSelectedRow);
            Messenger.Default.Register<ListIndex>(this, SetSelectedRow);


        }
    
        public int selectedRow = -1;
        private void SetSelectedRow(ListIndex li)
        {
            selectedRow = li.getValue();
            RefreshButton = true;
            BackgroundButton = true;
            //chargement des databases
            if (selectedRow != -1)
            {

                var thumb = Cuts.GetAt(selectedRow).Thumbnail;
                TeamOne = Cuts.GetAt(selectedRow).Thumbnail.TeamOne;
                TeamTwo = Cuts.GetAt(selectedRow).Thumbnail.TeamTwo;
                DbCharOne = Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase;
                DbCharTwo = Cuts.GetAt(selectedRow).Thumbnail.TeamTwo.DataBase;
                Debug.WriteLine(Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase[0].Name);
                Debug.WriteLine(Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase[0].IsPlayed);

                Background = new BitmapImage(thumb.Background);
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
                BackgroundButton = false;
                
                TeamOne = null;
                TeamTwo = null;
                DbCharOne = null;
                DbCharTwo = null;
            }
            
        }

        public RelayCommand ResetPreviewCommand { get; set; }
         private void ResetPreview()
         {
             // chargement de la miniature : on doit parcourir les 2 databases et afficher les perso checked
            Bitmap temp_image = new Bitmap(Cuts.GetAt(selectedRow).Thumbnail.Background.OriginalString);
            
            int width = temp_image.Width;
            int height = temp_image.Height;
            float sizeChar = width / 38.4F;
            Graphics g = Graphics.FromImage(temp_image);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //create the outline of the text 



            System.Drawing.FontFamily f = new System.Drawing.FontFamily("Arial");
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.White);
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 5);
            PointF drawPoint = new PointF(width/4 - (TeamOne.TeamName.Length * sizeChar) / 2, height * 3/4);
            GraphicsPath p = new GraphicsPath();
            //write team name 1
           
            p.AddString(
                TeamOne.TeamName,             // text to draw
                f,  // or any other font family
                (int)System.Drawing.FontStyle.Regular,
                g.DpiY * sizeChar / 72,       // em size
                drawPoint,              // location where to draw text
                new StringFormat());          // set options here (e.g. center alignment)
            g.DrawPath(pen, p);
            g.FillPath(drawBrush, p);
            
            
            //write team name 2
            drawPoint.X = temp_image.Width - width / 4 - (TeamTwo.TeamName.Length * sizeChar)/2;
            GraphicsPath p2 = new GraphicsPath();
            //write team name 1
            p2.AddString(
                TeamTwo.TeamName,             // text to draw
                f,  // or any other font family
                (int)System.Drawing.FontStyle.Regular,
                g.DpiY * sizeChar / 72,       // em size
                drawPoint,              // location where to draw text
                new StringFormat());          // set options here (e.g. center alignment)
            g.DrawPath(pen, p2);
            g.FillPath(drawBrush, p2);
            
            //multiplicateur de position
            List<int> posTab = new List<int>();
            // on vérifie une fois le tableau pour savoir combien de cases sont cochées
            for (int i = 0; i < DbCharOne.Count; i++)
            {
                if (DbCharOne[i].IsPlayed)
                {
                    posTab.Add(i);
                    
                    Debug.Write(Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase[i].Name + ": ");
                    Debug.WriteLine(Cuts.GetAt(selectedRow).Thumbnail.TeamOne.DataBase[i].IsPlayed);  

                }
            }
            // on parcours la liste des cases cochées
            for (int i = 0; i < posTab.Count; i++)
            {
               Character temp_char = DbCharOne[posTab[i]];
               int nb_char = posTab.Count;
               //afficher la miniature a gauche
               Bitmap char_picture = new Bitmap(temp_char.PictureUri.OriginalString);

               
                //mise a echelle
                int newWidth = (int)((char_picture.Width / (nb_char + 1)) * ((float)width / 1920));
                int newHeight = (int)((char_picture.Height / (nb_char + 1)) * ((float)width / 1920));
                Bitmap Smaller_char = new Bitmap(char_picture, new System.Drawing.Size(newWidth, newHeight));
                //Smaller_char.MakeTransparent();
                
               
               Rectangle rect = new Rectangle(width * (i + 1) / (2 + 2*nb_char) - Smaller_char.Width/2 , height/2 - (Smaller_char.Height) * 2/3, Smaller_char.Width, Smaller_char.Height);
               //image.WritePixels(rect, Data.Scan0, Data.Stride,100000);
               g.DrawImage(Smaller_char, rect);
               
            }

            posTab.Clear();
            
            // on vérifie une fois le tableau pour savoir combien de cases sont cochées
            for (int i = 0; i < DbCharTwo.Count; i++)
            {
                if (DbCharTwo[i].IsPlayed)
                {
                    posTab.Add(i);
                }
            }
            // on parcours la liste des cases cochées
            for (int i = 0; i < posTab.Count; i++)
            {
                Character temp_char = DbCharOne[posTab[i]];
                int nb_char = posTab.Count;
                //afficher la miniature a gauche

                Bitmap char_picture = new Bitmap(temp_char.PictureUri.OriginalString);
                int newWidth = (int)((char_picture.Width / (nb_char + 1)) * ((float)width / 1920));
                int newHeight = (int)((char_picture.Height / (nb_char + 1)) * ((float)width / 1920));
                Bitmap Smaller_char = new Bitmap(char_picture, new System.Drawing.Size(newWidth, newHeight));
                //Smaller_char.MakeTransparent();

                Smaller_char.RotateFlip(RotateFlipType.RotateNoneFlipX);
               

                Rectangle rect = new Rectangle(width - (width * (i + 1) / (2 + 2 * nb_char)) - Smaller_char.Width / 2, height / 2 - (Smaller_char.Height) * 2 / 3, Smaller_char.Width, Smaller_char.Height);
                //image.WritePixels(rect, Data.Scan0, Data.Stride,100000);
                g.DrawImage(Smaller_char, rect);

            }
            //change pas grand chose
            g.Dispose();


            IntPtr hBitmap = temp_image.GetHbitmap();
            try
            {
                var img_src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                       hBitmap, IntPtr.Zero, Int32Rect.Empty,
                       System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                WriteableBitmap wb = new WriteableBitmap(img_src);
                var imgs = new ImageSend(wb);
                Messenger.Default.Send(imgs);
            }
            finally
            {
                DeleteObject(hBitmap);
            }
            
          
           

            
            //change pas grand chose
            temp_image.Dispose();

     

          
           


         } 
                



      /*  private void ResetPreview()
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
        } */

        public void ChangeBackGround(NotificationMessage nm)
        {
            if(nm.Notification == "newBackGround")
            {
                Cuts.GetAt(selectedRow).Thumbnail.Background = new Uri((string)nm.Sender);
            }
        }
        public RelayCommand btnOpenBack_Click { get; set; }
        private void OpenB()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers image (*.jpg, *.png)| *.jpg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                if (ValidFile(openFileDialog.FileName, 1920, 1080))
                {

                    
                    Cuts.GetAt(selectedRow).Thumbnail.Background = new Uri(openFileDialog.FileName);
                    Background = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                else
                {
                    MessageBox.Show("L'image doit être en 16:9");
                }
            }

        }

        private bool ValidFile(string filename, int limitWidth, int limitHeight)
        {
            var fileSizeInBytes = new FileInfo(filename).Length;
            using (var img = new Bitmap(filename))
            {
                if ((int)(img.Width / img.Height) == (int)16/9 ) return true;
            } 

            return false;
        }




    }
}
