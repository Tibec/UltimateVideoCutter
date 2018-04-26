using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VideoCutterMax2.Model
{
    class Video
    {
        [XmlIgnore]
        public Uri Uri { get; set; }
        public String Name { get; set; }

        public Video(Uri u, String n)
        {
            this.Uri = u ;
            this.Name = n; 
        }
    }
}
