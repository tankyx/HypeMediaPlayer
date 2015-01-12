using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HMP.Desktop.Module.Models
{
    public class Model
    {
        private Media _currentMedia;
        private List<Media> _mediaList;
        public List<Media> MediaList
        {
            get { return _mediaList; }
            set { _mediaList = value; }
        }
        public Media CurrentMedia
        {
            get { return _currentMedia; }
            set { _currentMedia = value; }
        }
        public Model()
        {
            _mediaList = new List<Media>();
        }
    }
    public class Media
    {
        [XmlElement("fullPath")]
        public string FullPath { get; set; }
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string name { get; set; }
    }
}
