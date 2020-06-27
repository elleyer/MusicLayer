using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Events
{
    class TrackEventHandler
    {
        internal int Stream { get; private set; }

        internal void OnTrackChanged()
        {
         
        }

        internal void OnTrackEnded()
        {

        }

        internal void OnVolumeChanged()
        {

        }

        internal void SetStream(int value) => Stream = value;
    }
}
