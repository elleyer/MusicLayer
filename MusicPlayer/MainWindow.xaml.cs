using ManagedBass;
using Microsoft.Win32;
using MusicPlayer.Audio;
using MusicPlayer.Clock;
using MusicPlayer.Data;
using MusicPlayer.FileSystem;
using MusicPlayer.UI;
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
        internal AudioManager AudioManager;

        internal FileLoader FileLoader;

        internal MainWindowController MainWindowController;

        public MainWindow()
        {
            InitializeComponent();

            AudioManager = new AudioManager();

            MainWindowController = new MainWindowController(Dispatcher, AudioManager,
                MusicPositionSlider, MasterVolumeSlider, TimelinePlaybackValue);
        }

        private void PlayOrPauseButton(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (AudioManager.State)
            {
                case State.Playing:
                    PlayOrPauseButtonUI.Content = Constants.PLAY_STATE;

                    AudioManager.SetState(State.Paused);

                    MainWindowController.StopUpdate();

                    break;

                case State.Paused:
                    PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

                    AudioManager.SetState(State.Playing);

                    MainWindowController.Update();
 

                    break;

                case State.Idle:
                    PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

                    AudioManager.SetState(State.Playing);

                    MainWindowController.Update();

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

                var file = FileLoader.Files.
                    FirstOrDefault(x => x.FileName == MusicListBox.SelectedItem.ToString());

                AudioManager.SetTrack(file);

                MainWindowController.Update();

                PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

                CurrentTrackTextBlock.Text = file.FileName;
            }
        }

        private void OnTimelineSliderValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (!MusicPositionSlider.IsMouseOver)
                return;

            AudioManager?.SetPosition(MusicPositionSlider.Value);
        }

        private void OnVolumeSliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            AudioManager?.SetVolume(MasterVolumeSlider.Value);
        }
    }
}