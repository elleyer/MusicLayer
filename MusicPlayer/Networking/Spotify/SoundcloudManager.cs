using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedBass;
using SoundCloud.Api;

namespace MusicPlayer.Networking.Spotify
{
    internal sealed class SoundcloudManager
    {
        internal SoundcloudManager(string clientId )
        {
            var client = SoundCloudClient.CreateUnauthorized(clientId);
        }
    }
}
