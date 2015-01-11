using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HMP.Desktop.ImageViewer.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Uri _imgPath;
        private string _currFileName;
        private Images _currImages;
        private List<Images> _imgList = new List<Images>();

        public Uri ImageToShow
        {
            get
            {
                return _imgPath;
            }
            set
            {
                _imgPath = value;
                OnPropertyChanged("ImageToShow");
            }
        }

        public string FileName
        {
            get
            {
                return _currFileName;
            }
            set
            {
                _currFileName = value;
                OnPropertyChanged("FileName");
            }
        }

        RelayCmd _openFileCmd;
        public ICommand OpenFileCmd
        {
            get
            {
                if (_openFileCmd == null)
                    _openFileCmd = new RelayCmd(p => getImage(), p => true);
                return _openFileCmd;
            }
        }
        void getImage()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Image (.jpg)|*.jpg|PNG Image|*.png|Gif Image (.gif)|*.gif|Bitmap Image (.bmp)|*.bmg";
            dlg.Multiselect = true;
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                if (_imgList != null)
                    _imgList.Clear();
                foreach (string file in dlg.FileNames)
                {
                    _imgList.Add(new Images { name = System.IO.Path.GetFileName(file), path = new Uri (file) });
                }
                ImageToShow = _imgList.First().path;
                _currImages = _imgList.First();
                FileName = _imgList.First().name;
            }
        }
        RelayCmd _nextImgCmd;
        public ICommand NextImgCmd
        {
            get
            {
                if (_nextImgCmd == null)
                    _nextImgCmd = new RelayCmd(p => GoToNext(), p => true);
                return _nextImgCmd;
            }
        }

        private void GoToNext()
        {
            Images tmp;

            if (_imgList.IndexOf(_currImages) == _imgList.Count - 1)
                tmp = _imgList[0];
            else
                tmp = _imgList[_imgList.IndexOf(_currImages) + 1];
            _currImages = tmp;
            ImageToShow = _currImages.path;
            FileName = _currImages.name;
        }
        RelayCmd _prevImgCmd;
        public ICommand PrevImgCmd
        {
            get
            {
                if (_prevImgCmd == null)
                    _prevImgCmd = new RelayCmd(p => GoToPrev(), p => true);
                return _prevImgCmd;
            }
        }

        private void GoToPrev()
        {
            Images tmp;

            if (_imgList.IndexOf(_currImages) == 0)
                tmp = _imgList[_imgList.Count - 1];
            else
                tmp = _imgList[_imgList.IndexOf(_currImages) - 1];
            _currImages = tmp;
            ImageToShow = _currImages.path;
            FileName = _currImages.name;
        }
        protected void OnPropertyChanged(string p)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(p));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class Images
    {
        public Uri path;
        public string name;
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
