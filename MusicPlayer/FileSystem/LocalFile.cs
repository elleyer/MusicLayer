using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.FileSystem
{
    internal class LocalFile
    {
        internal string FileName;

        internal string Path;

        internal long Length;

        internal LocalFile(string path, string fileName, long length)
        {
            Path = path;

            FileName = fileName;

            Length = length;
        }
    }
}
