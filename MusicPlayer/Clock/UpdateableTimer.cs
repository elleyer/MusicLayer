using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicPlayer.Clock
{
    internal class UpdateableTimer
    {
        internal const int UPDATE_INTERVAL = 1;
        internal const int UPDATE_DUE_TIME = 1;

        internal Timer Timer;

        internal virtual void Update(Timer timer)
        {
            Timer = timer;

            Timer.Change(UPDATE_DUE_TIME, UPDATE_INTERVAL);
        }

        internal virtual void StopUpdate()
        {
            Timer.Dispose();
        }
    }
}
