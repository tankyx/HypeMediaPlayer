using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HMP.Desktop.Module.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private MediaElement player;
        private Uri _source;
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

        void setSource()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MPEG 4 video (.mp4)|*.mp4|Matroska Video (.mkv)|*.mkv|Avi Video (*.avi)|*.avi";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                SourceToPlay = new Uri (dlg.FileName);
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

        void PlayMedia(object param)
        {
            player = (MediaElement)param;
            if (SourceToPlay == null)
            {
                setSource();
                player.Source = SourceToPlay;
            }
            player.Play();
        }

        void PauseMedia(object param)
        {
            player = (MediaElement)param;
            player.Pause();
        }

        protected void OnPropertyChanged(string p)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(p));
        }
        public event PropertyChangedEventHandler PropertyChanged;
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