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

        internal AudioManager()
        {
            Bass.Init();
        }

        internal void Play()
        {
            State = State.Playing;

            if (Stream != null)
                Bass.ChannelPlay(Stream);
        }

        internal void Pause()
        {

            State = State.Paused;

            if (Stream != null)
                Bass.ChannelPause(Stream);
        }

        internal void Stop()
        {
            State = State.Idle;

            if (Stream != null)
                Bass.ChannelStop(Stream);

            Bass.Free();
        }

        internal void SetTrack(LocalFile localFile)
        {
            Bass.Init();

            Bass.ChannelStop(Stream);

            Stream = Bass.CreateStream(localFile.Path);

            ClockContainer.SetStream(Stream);

            ClockContainer.Update();

            Play();
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
