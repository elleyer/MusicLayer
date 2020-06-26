using ManagedBass;
using Microsoft.Win32;
using MusicPlayer.Audio;
using MusicPlayer.Clock;
using MusicPlayer.Data;
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

        public MainWindow()
        {
            InitializeComponent();

            AudioLoader = new AudioManager();

            ClockContainer = new ClockContainer(MusicPositionSlider, 
                TimelinePlaybackValue, AudioLoader.Stream, Dispatcher);

            AudioLoader.ClockContainer = ClockContainer;
        }

        private void PlayOrPauseButton(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (AudioLoader.State)
            {
                case State.Playing:
                    PlayOrPauseButtonUI.Content = Constants.PLAY_STATE;

                    AudioLoader.SetState(State.Paused);

                    ClockContainer.StopUpdate();

                    break;

                case State.Paused:
                    PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

                    AudioLoader.SetState(State.Playing);

                    ClockContainer.Update();
 

                    break;

                case State.Idle:
                    PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

                    AudioLoader.SetState(State.Playing);

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

                var file = FileLoader.Files.
                    FirstOrDefault(x => x.FileName == MusicListBox.SelectedItem.ToString());

                AudioLoader.SetTrack(file);

                PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

                CurrentTrackTextBlock.Text = file.FileName;
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