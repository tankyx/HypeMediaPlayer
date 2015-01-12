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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using HMP.Desktop.Module.Models;

namespace HMP.Desktop.Module.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private MediaElement _player;
        private Uri _source;
        private Uri _image;
        [XmlElement("Media")]
        private Model model = new Model();
        private string _currMediaName;
        private static bool Initialized { get; set; }

        public string CurrentFileName
        {
            get
            {
                if (_currMediaName != null)
                    return _currMediaName;
                else
                    return "";
            }
            set
            {
                _currMediaName = value;
                OnPropertyChanged("CurrentFileName");
            }
        }

        public Uri SourceToPlay
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged("SourceToPlay");
            }
        }

        public Uri ImageToShow
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("ImageToShow");
            }
        }

        void setMediaList(string listName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Models.Media>));
            if (model.MediaList != null)
                model.ClearMediaList();
            using (System.IO.StreamReader fileNameXml = new System.IO.StreamReader(listName))
            using (var reader = XmlReader.Create(fileNameXml))
            {
                model.MediaList = (List<Models.Media>)ser.Deserialize(reader);
            }
        }
        void setSource()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MPEG 4 video (.mp4)|*.mp4|Matroska Video (.mkv)|*.mkv|Avi Video (*.avi)|*.avi|MP3 Music (.mp3)|*.mp3|JPG Image (.jpg)|*.jpg|Playlist File (.xml)|*.xml";

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
                    model.MediaList.Add(new Models.Media() { name = dlg.SafeFileName, FullPath = dlg.FileName, Id = 0 });
                }
                model.CurrentMedia = model.MediaList.First();
                CurrentFileName = model.CurrentMedia.name;
                SourceToPlay = new Uri(model.MediaList.First().FullPath);
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
                    _changeSourceCmd = new RelayCmd(p => ChangeSource(),
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
                    _nextMedia = new RelayCmd(p => NextMedia(), p => true);
                return _nextMedia;
            }
        }
        RelayCmd _prevMedia;
        public ICommand PrevMediaCmd
        {
            get
            {
                if (_prevMedia == null)
                    _prevMedia = new RelayCmd(p => PrevMedia(), p => true);
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

        void ChangeSource()
        {
            setSource();
        }

        void NextMedia()
        {
            Models.Media tmp;

            if (model.MediaList.IndexOf(model.CurrentMedia) == model.MediaList.Count - 1)
                tmp = model.MediaList[0];
            else
                tmp = model.MediaList[model.MediaList.IndexOf(model.CurrentMedia) + 1];
            model.CurrentMedia = tmp;
            CurrentFileName = model.CurrentMedia.name;
            SourceToPlay = new Uri(model.CurrentMedia.FullPath);
        }
        void PrevMedia()
        {
            Models.Media tmp;

            if (model.MediaList.IndexOf(model.CurrentMedia) == 0)
                tmp = model.MediaList[model.MediaList.Count - 1];
            else
                tmp = model.MediaList[model.MediaList.IndexOf(model.CurrentMedia) - 1];
            model.CurrentMedia = tmp;
            CurrentFileName = model.CurrentMedia.name;
            SourceToPlay = new Uri(model.CurrentMedia.FullPath);
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