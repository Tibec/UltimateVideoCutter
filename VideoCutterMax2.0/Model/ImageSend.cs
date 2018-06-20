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
        public int[] mult;

        public ImageSend(WriteableBitmap wp, int[] i)
        {
            image = wp;
            mult = i;
        }
    }
}
