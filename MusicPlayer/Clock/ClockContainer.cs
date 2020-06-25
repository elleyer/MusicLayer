using ManagedBass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MusicPlayer.Clock
{
    class ClockContainer
    {
        internal Timer Timer;

        internal Slider Slider;

        internal TextBlock TextBlock;

        internal int Stream;

        private Dispatcher _dispatcher;

        internal ClockContainer(Slider slider, TextBlock textBlock, int stream, Dispatcher dispather)
        {
            Slider = slider;
            TextBlock = textBlock;
            _dispatcher = dispather;
        }

        internal void Update()
        {
            Timer = new Timer(UpdateDrawableItems);

            Slider.Minimum = 0;
            Slider.Maximum = (double)Bass.ChannelGetLength(Stream);

            Timer.Change(1, 1);
        }

        internal void SetStream(int stream)
        {
            Stream = stream;
        }

        internal void Reset()
        {
            if(Timer != null)
               Timer.Dispose();
        }

        internal void StopUpdate()
        {

        }

        private void UpdateDrawableItems(object state)
        {
            _dispatcher.Invoke(() =>
            {
                var position = Bass.ChannelGetPosition(Stream);
                var span = TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(Stream, position));

                TextBlock.Text = string.Format("{0}:{1:00}",
                            (int)span.TotalMinutes,
                            span.Seconds);

                if (Slider.IsMouseOver)
                    return;

                Slider.Value = position;
                Slider.UpdateLayout();
            });
        }
    }
}
