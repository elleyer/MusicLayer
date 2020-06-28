using ManagedBass;
using Microsoft.Win32;
using MusicPlayer.Audio;
using MusicPlayer.Clock;
using MusicPlayer.Data;
using MusicPlayer.FileSystem;
using MusicPlayer.UI;
using System;
using System.IO;
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

            AudioManager.OnTrackEnded += OnTrackEndedHandler; //Unsubscribe using Disposable pattern.

            MainWindowController = new MainWindowController(Dispatcher, AudioManager,
                MusicPositionSlider, MasterVolumeSlider, TimelinePlaybackValue);
        }

        private void UpdateTrack(LocalFile file)
        {
            AudioManager.SetTrack(file);

            AudioManager.CurrentTrackID = FileLoader.Files.IndexOf(file);

            MainWindowController.SetTimelineValues(0,
                AudioManager.AudioProperties.PlaybackLength);

            MainWindowController.Update();

            PlayOrPauseButtonUI.Content = Constants.PAUSE_STATE;

            CurrentTrackTextBlock.Text = file.FileName;
        }

        private void OnTrackEndedHandler()
        {
            switch (AudioManager.AudioProperties.Looped)
            {
                case true:
                    UpdateTrack(FileLoader.Files[AudioManager.CurrentTrackID]);
                    break;
                case false:
                    UpdateTrack(FileLoader.Files[AudioManager.CurrentTrackID + 1]);
                    break;
            }
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

                UpdateTrack(file);
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

        private void OnRepeatButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            AudioManager.AudioProperties.SetLoopValue((bool)LoopedCheckbox.IsChecked);
        }
    }
}