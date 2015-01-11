using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections;
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
using System.Xml.Serialization;

namespace HMP.Desktop.Module.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private MediaElement _player;
        private Uri _source;
        [XmlElement("Media")]
        private List<Media> _playlist;
        private Media _currentMedia;
        private TimeSpan _seekToMedia = TimeSpan.FromSeconds(1);
        private static bool Initialized { get; set; }

        public Uri SourceToPlay
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged("SourceToPlay");
            }
        }

        void setMediaList(string listName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Media>));
            if (_playlist != null)
                _playlist.Clear();
            using (System.IO.StreamReader fileNameXml = new System.IO.StreamReader(listName))
            using (var reader = XmlReader.Create(fileNameXml))
            {
                _playlist = (List<Media>)ser.Deserialize(reader);
            }
        }
        void setSource()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MPEG 4 video (.mp4)|*.mp4|Matroska Video (.mkv)|*.mkv|Avi Video (*.avi)|*.avi|Playlist File (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Debug.Write("Extention playlist = " + System.IO.Path.GetExtension(dlg.SafeFileName) + "\n");
                if (System.IO.Path.GetExtension(dlg.SafeFileName) == ".xml")
                {
                    setMediaList(dlg.FileName);
                }
                else
                {
                    _playlist = new List<Media>();
                    _playlist.Add(new Media() { name = dlg.SafeFileName, FullPath = dlg.FileName, Id = 0 });
                }
                _currentMedia = _playlist.First();
                SourceToPlay = new Uri (_playlist.First().FullPath);
            }
        }

        RelayCmd _openDlg;
        public ICommand OpenDlg
        {
            get
            {
                if (_openDlg == null)
                    _openDlg = new RelayCmd(p => setSource(), p => true);
                return _openDlg;
            }
        }

        RelayCmd _playMediaCmd;
        public ICommand PlayMediaCmd
        {
            get
            {
                if (_playMediaCmd == null)
                {
                    _playMediaCmd = new RelayCmd(p => PlayMedia(p),
                        p => true);
                }
                return _playMediaCmd;
            }
        }

        RelayCmd _pauseMediaCmd;
        public ICommand PauseMediaCmd
        {
            get
            {
                if (_pauseMediaCmd == null)
                {
                    _pauseMediaCmd = new RelayCmd(p => PauseMedia(p),
                        p => true);
                }
                return _pauseMediaCmd;
            }
        }

        RelayCmd _changeSourceCmd;
        public ICommand ChangeSourceCmd
        {
            get
            {
                if (_changeSourceCmd == null)
                {
                    _changeSourceCmd = new RelayCmd(p => ChangeSource(p),
                        p => true);
                }
                return _changeSourceCmd;
            }
        }

        RelayCmd _nextMedia;
        public ICommand NextMediaCmd
        {
            get
            {
                if (_nextMedia == null)
                    _nextMedia = new RelayCmd(p => NextMedia(p), p => true);
                return _nextMedia;
            }
        }
        RelayCmd _prevMedia;
        public ICommand PrevMediaCmd
        {
            get
            {
                if (_prevMedia == null)
                    _prevMedia = new RelayCmd(p => PrevMedia(p), p => true);
                return _prevMedia;
            }
        }
        void PlayMedia(object param)
        {
            _player = (MediaElement)param;
            if (SourceToPlay == null)
            {
                setSource();
            }
            _player.Play();
        }

        void PauseMedia(object param)
        {
            _player = (MediaElement)param;
            _player.Pause();
        }

        void ChangeSource(object param)
        {
            _player = (MediaElement)param;
            setSource();
        }

        void NextMedia(object param)
        {
            Media tmp;

            _player = (MediaElement)param;
            if (_playlist.IndexOf(_currentMedia) == _playlist.Count - 1)
                tmp = _playlist[0];
            else
                tmp = _playlist[_playlist.IndexOf(_currentMedia) + 1];
            _currentMedia = tmp;
            SourceToPlay = new Uri (_currentMedia.FullPath);
        }
        void PrevMedia(object param)
        {
            Media tmp;

            _player = (MediaElement)param;
            if (_playlist.IndexOf(_currentMedia) == 0)
                tmp = _playlist[_playlist.Count - 1];
            else
                tmp = _playlist[_playlist.IndexOf(_currentMedia) - 1];
            _currentMedia = tmp;
            SourceToPlay = new Uri(_currentMedia.FullPath);
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