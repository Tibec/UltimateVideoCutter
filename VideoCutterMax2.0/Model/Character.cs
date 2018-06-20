using System;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoCutterMax2.Model
{
    class Character : ObservableObject
    {
        
        public Uri PictureUri { get; set; }
        public String Name { get; set; }
        private Boolean _isPlayed;
        public Boolean IsPlayed
        {
            get { return _isPlayed; }
            set { Set("isPlayed", ref _isPlayed, value); }
        }
        public Character(Uri u, String n)
        {
            
            PictureUri = u;
            Name = n;
            IsPlayed = false;
        }
        public Character( String n)
        {
           
            Name = n;
        }
    }
}
