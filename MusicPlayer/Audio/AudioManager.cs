using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedBass;
using MusicPlayer.Clock;
using MusicPlayer.FileSystem;
using GeniusAPIWrapper;
using GeniusAPIWrapper.Requests;
using GeniusAPIWrapper.JsonData.Search;

namespace MusicPlayer.Audio
{
    internal class AudioManager : IDisposable
    {
        internal delegate void TrackEnded();

        internal event TrackEnded OnTrackEnded;

        internal State State = State.Idle;

        internal int Stream { get; private set; }

        internal int CurrentTrackID { get; set; }

        internal AudioProperties AudioProperties = default;

        internal FileLoader FileLoader;

        internal AudioManager()
        {
            Bass.Init();

            AudioProperties = new AudioProperties();
        }

        internal void LoadFiles(FileLoader fileloader)
        {
            FileLoader = fileloader;
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

            AudioProperties.PlaybackLength = Bass.ChannelGetLength(Stream);

            SetState(State.Playing);
        }

        internal void SetPosition(double value)
        {
            Bass.ChannelSetPosition(Stream, (long)value);
        }

        internal void SetVolume(double volumeLevel)
        {
            Bass.GlobalStreamVolume = ((int)volumeLevel * 100);
        }

        internal void SongStatusEnded()
        {
            OnTrackEnded.Invoke();
        }

        void IDisposable.Dispose()
        {
            
        }
    }

    internal enum State
    {
        Playing = 0,
        Idle = 1,
        Paused = 2
    }
}
