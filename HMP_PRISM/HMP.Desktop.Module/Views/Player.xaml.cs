using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HMP.Desktop.Module.Views
{
    /// <summary>
    /// Interaction logic for ModuleView.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        private DispatcherTimer Ticker;
        public Player()
        {
            InitializeComponent();

            ViewModels.ViewModel vm = new ViewModels.ViewModel();

            this.DataContext = vm;
            posMedia.ApplyTemplate();
            Thumb thumb = (posMedia.Template.FindName("PART_Track", posMedia) as Track).Thumb;
            thumb.MouseEnter += new MouseEventHandler(thumb_MouseEnter);

            Debug.WriteLine("My Message = Tick");
            Ticker = new DispatcherTimer();
            Ticker.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Ticker.Tick += Tick;
            Ticker.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            posMedia.ValueChanged -= posMedia_ValueChanged;
            posMedia.Value = player.Position.TotalMilliseconds;
            posMedia.ValueChanged += posMedia_ValueChanged;
        }
        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            posMedia.Maximum = player.NaturalDuration.TimeSpan.TotalMilliseconds;
        }
        private void MediaTimeChanged(object sender, EventArgs e)
        {
            posMedia.Value = player.Position.TotalMilliseconds;
        }

        private void posMedia_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Position = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(posMedia.Value));
        }
    }
}
