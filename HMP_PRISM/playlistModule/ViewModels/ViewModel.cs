using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HMP.Desktop.PlaylistModule.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private List<string> _playlist = new List<string>();
        private ListView _list;
        [XmlElement("Media")]
        private List<Media> _MediaList = new List<Media>();
        private string _listname = "";
        private static int _id = 1;
        ObservableCollection<Media> _MediaCollection = new ObservableCollection<Media>();
        public ObservableCollection<Media> MediaCollection
        { get { return _MediaCollection; } }

        RelayCmd _addFile;
        public ICommand AddFileCmd
        {
            get
            {
                if (_addFile == null)
                    _addFile = new RelayCmd(p => multiOpenFile(p), p => true);
                return _addFile;
            }
        }

        RelayCmd _delFile;
        public ICommand DelFileCmd
        {
            get
            {
                if (_delFile == null)
                    _delFile = new RelayCmd(p => DelFile(p), p => true);
                return _delFile;
            }
        }

        void DelFile(object param)
        {
            _list = (ListView)param;
            var toBeDel = _list.SelectedItems;
            if (toBeDel.Count != 0)
            {
                for (int i = toBeDel.Count - 1; i >= 0; i--)
                {
                    _MediaList.Remove((Media)toBeDel[i]);
                    _MediaCollection.Remove((Media)toBeDel[i]);
                }
            }
        }
        public string PlaylistName
        {
            get { return _listname; }
            set
            {
                _listname = value;
                OnPropertyChanged("PlaylistName");
            }
        }
        private void multiOpenFile(object param)
        {
            _list = (ListView)param;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Multiselect = true;
            dlg.Filter = "MPEG 4 video (.mp4)|*.mp4|Matroska Video (.mkv)|*.mkv|Avi Video (.avi)|*.avi|MP3 Audio (.mp3)|*.mp3";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                foreach(String file in dlg.FileNames)
                {
                    _MediaList.Add(new Media() { name = System.IO.Path.GetFileName(file), FullPath = file });
                    _MediaCollection.Add(new Media() { name = System.IO.Path.GetFileName(file), FullPath = file });
                    _playlist.Add(file);
                }
            }
        }

        RelayCmd _saveList;
        public ICommand SaveListCmd
        {
            get
            {
                if (_saveList == null)
                    _saveList = new RelayCmd(p => openSaveDlg(p), p => true);
                return _saveList;
            }
        }

        private void openSaveDlg(object param)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(_playlist.GetType());
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "playlist";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Playlist files (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                _listname = dlg.FileName;
                XmlSerializer ser = new XmlSerializer(typeof(List<Media>));
                System.IO.StreamWriter fileNameXml = new System.IO.StreamWriter(_listname);
                ser.Serialize(fileNameXml, _MediaList);
            }
        }

        RelayCmd _openList;
        public ICommand OpenListCmd
        {
            get
            {
                if (_openList == null)
                    _openList = new RelayCmd(p => openXmlFile(p), p => true);
                return _openList;
            }
        }

        private void openXmlFile(object param)
        {
            _list = (ListView)param;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Multiselect = false;
            dlg.Filter = "Playlist File (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                PlaylistName = dlg.SafeFileName;
                XmlSerializer ser = new XmlSerializer(typeof(List<Media>));
                _MediaList.Clear();
                _MediaCollection.Clear();
                using (System.IO.StreamReader fileNameXml = new System.IO.StreamReader(dlg.FileName))
                using (var reader = XmlReader.Create(fileNameXml))
                {
                    _MediaList = (List<Media>)ser.Deserialize(reader);
                }
                foreach (var med in _MediaList)
                {
                    _MediaCollection.Add(med);
                }
            }
        }
 
        protected void OnPropertyChanged(string p)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(p));
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
    class RelayCmd : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public RelayCmd(Action<object> execute)
            : this(execute, null)
        { }

        public RelayCmd(Action<object> execute,
                       Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {

            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
