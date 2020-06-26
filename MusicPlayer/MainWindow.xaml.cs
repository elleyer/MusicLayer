using ManagedBass;
using Microsoft.Win32;
using MusicPlayer.Audio;
using MusicPlayer.Clock;
using MusicPlayer.FileSystem;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Forms;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        internal AudioManager AudioLoader;

        internal FileLoader FileLoader;

        internal ClockContainer ClockContainer;

        internal static bool Snapping;

        public MainWindow()
        {
            InitializeComponent();

            AudioLoader = new AudioManager();

            ClockContainer = new ClockContainer(MusicPositionSlider, TimelinePlaybackValue, AudioLoader.Stream, Dispatcher);

            AudioLoader.ClockContainer = ClockContainer;
        }

        private void PlayOrPauseButton(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (AudioLoader.State)
            {
                case State.Playing:
                    PlayOrPauseButtonUI.Content = "Play";

                    AudioLoader.Pause();

                    ClockContainer.StopUpdate();

                    break;

                case State.Paused:
                    PlayOrPauseButtonUI.Content = "Pause";

                    AudioLoader.Play();

                    ClockContainer.Update();
 

                    break;

                case State.Idle:
                    PlayOrPauseButtonUI.Content = "Pause";

                    AudioLoader.Play();

                    ClockContainer.Update();

                    break;
            }

            PlayOrPauseButtonUI.UpdateLayout();
        }

        private void OnClickLoadButton(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectFolder = new FolderBrowserDialog();

            selectFolder.ShowDialog();

            if (selectFolder.SelectedPath == string.Empty)
                return;

            FileLoader = new FileLoader(selectFolder.SelectedPath); //Dispose

            FileLoader.LoadToList(MusicListBox);
        }

        private void OnItemMouseDoubleClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(MusicListBox.SelectedItem != null)
            {
                var dataContext = MusicListBox.DataContext;

                ClockContainer.Reset();

                AudioLoader.SetTrack(FileLoader.Files.
                    FirstOrDefault(x => x.FileName == MusicListBox.SelectedItem.ToString()));

                PlayOrPauseButtonUI.Content = "Pause";
                PlayOrPauseButtonUI.UpdateLayout();
            }
        }

        private void OnValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (!MusicPositionSlider.IsMouseOver)
                return;

            AudioLoader.SetPosition(MusicPositionSlider.Value);
        }
    }
}