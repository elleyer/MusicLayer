using MusicPlayer.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Utilities
{
    internal static class EnumUtils
    {
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (string.IsNullOrEmpty(value)) return default(T);

            return Enum.TryParse<T>(value, true, out T result) ? result : default(T);
        }
    }
}
