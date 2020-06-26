using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedBass;
using MusicPlayer.Clock;
using MusicPlayer.FileSystem;

namespace MusicPlayer.Audio
{
    internal class AudioManager
    {
        internal State State = State.Idle;

        internal int Stream;

        internal ClockContainer ClockContainer;

        internal AudioProperties AudioProperties;

        internal AudioManager()
        {
            Bass.Init();
        }

        internal void SetState(State state)
        {
            switch (state)
            {
                case State.Playing:
                    State = State.Playing;

                    Bass.ChannelPlay(Stream);
                    break;

                case State.Paused:
                    State = State.Paused;

                    Bass.ChannelPause(Stream);
                    break;
            }        
        }

        internal void SetTrack(LocalFile localFile)
        {
            Bass.Init();

            Bass.ChannelStop(Stream);

            Stream = Bass.CreateStream(localFile.Path);

            ClockContainer.SetStream(Stream);

            ClockContainer.Update();

            SetState(State.Playing);
        }

        internal void SetPosition(double value)
        {
            Bass.ChannelSetPosition(Stream, (long)value);
        }
    }

    internal enum State
    {
        Playing = 0,
        Idle = 1,
        Paused = 2
    }
}
