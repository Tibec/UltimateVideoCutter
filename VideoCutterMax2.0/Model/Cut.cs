using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoCutterMax2.Model
{
    class Cut : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }
        private TimeSpan _begin;
        public TimeSpan Begin
        {
            get { return _begin; }
            set { Set("Begin", ref _begin, value); }
        }
        private TimeSpan _end;
        public TimeSpan End
        {
            get { return _end; }
            set { Set("End", ref _end, value); }
        }

        private Miniature _thumbnail;
        public Miniature Thumbnail
        {
            get { return _thumbnail; }
            set { Set("Thumbnail", ref _thumbnail, value); }
        }


    }
}
