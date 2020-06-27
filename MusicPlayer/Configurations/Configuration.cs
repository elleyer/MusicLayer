using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MusicPlayer.Configurations.Sorting;

namespace MusicPlayer.Configurations
{
    internal class Configuration
    {
        internal int VolumeLevel;

        internal bool LoopedPlaying;
        internal bool AutoNext;

        internal Sorting.Sorting Sorting;
    }
}
