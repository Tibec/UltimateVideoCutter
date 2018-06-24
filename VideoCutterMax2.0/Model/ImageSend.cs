using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace VideoCutterMax2.Model
{
    class ImageSend
    {

        public WriteableBitmap image;
        

        public ImageSend(WriteableBitmap wp)
        {
            image = wp;
           
        }
    }
}
