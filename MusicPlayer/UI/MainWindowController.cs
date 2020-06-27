using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using ManagedBass;
using MusicPlayer.Audio;
using MusicPlayer.Clock;

namespace MusicPlayer.UI
{
    internal sealed class MainWindowController : UpdateableTimer
    {
        private Dispatcher _dispatcher;

        private AudioManager _audioManager;

        internal Slider TimelineSlider;
        internal Slider VolumeSlider;

        internal TextBlock CurrentPlaybackValue;

        internal MainWindowController(Dispatcher dispatcher, AudioManager audioManager,
             Slider timelineSlider, Slider volumeSlider, TextBlock currentPlaybackValueBlock)
        {
            _dispatcher = dispatcher;

            _audioManager = audioManager;

            TimelineSlider = timelineSlider;
            VolumeSlider = volumeSlider;

            CurrentPlaybackValue = currentPlaybackValueBlock;
        }

        internal void SetTimelineValues(double min, double max)
        {
            TimelineSlider.Minimum = min;
            TimelineSlider.Maximum = max;
        }

        internal void Update()
        {
            Update(new Timer(UpdateDrawableItems));
        }

        private void UpdateDrawableItems(object state)
        {
            try
            {
                _dispatcher.Invoke(() =>
                {
                    var position = Bass.ChannelGetPosition(_audioManager.Stream);
                    var span = TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(_audioManager.Stream, position));

                    CurrentPlaybackValue.Text = string.Format("{0}:{1:00}",
                                (int)span.TotalMinutes,
                                span.Seconds);

                    if (TimelineSlider.IsMouseOver)
                        return;

                    TimelineSlider.Value = position;
                    TimelineSlider.UpdateLayout();
                });
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
