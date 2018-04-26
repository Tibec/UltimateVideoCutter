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
        private string _begin;
        public string Begin
        {
            get { return _begin; }
            set { Set("Begin", ref _begin, value); }
        }
        private string _end;
        public string End
        {
            get { return _end; }
            set { Set("End", ref _end, value); }
        }

    }
}
