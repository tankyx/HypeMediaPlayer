using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace HypeMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static bool Initialized { private set; get; }
        private TimeSpan TotalTime;

        private String getMedia()
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MPEG 4 video (.mp4)|*.mp4|Matroska Video (.mkv)|*.mkv|Avi Video (*.avi)|*.avi";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                return (dlg.FileName);
            }
            return "";
        }

        private void playMedia(object sender, RoutedEventArgs e)
        {
            if (Initialized == false)
            {
                player.Source = new Uri(getMedia(), UriKind.RelativeOrAbsolute);
                Initialized = true;
            }

            player.Play();
        }

        private void pauseMedia(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void changeSoundLvl(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = (double)sound.Value;
        }

        private void moveMediaPos(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)posMedia.Value;

            //player.Position = TimeSpan.FromSeconds(posMedia.Value * TotalTime.TotalSeconds);
        }

        private void setSource(object sender, RoutedEventArgs e)
        {
            player.Source = new Uri(getMedia(), UriKind.RelativeOrAbsolute);
            Initialized = true;
            playMedia(null, null);
        }
    }
}
