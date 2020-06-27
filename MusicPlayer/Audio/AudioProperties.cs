using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Audio
{
    class AudioProperties
    {
        internal bool Looped { get; private set; }

        internal long PlaybackLength { get; set; }

        internal void SetLoopValue(bool value) => Looped = value;
    }
}
