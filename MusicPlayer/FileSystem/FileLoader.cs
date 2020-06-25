using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using ManagedBass;

namespace MusicPlayer.FileSystem
{
    internal class FileLoader
    {
        internal List <LocalFile> Files = new List<LocalFile>();

        internal FileLoader(string folder)
        {
            var files = Directory.GetFiles(folder);

            foreach(var file in files)
            {
                if(Path.GetExtension(file) == ".mp3")
                {
                    Bass.Init();

                    var stream = Bass.CreateStream(file);

                    Files.Add(new LocalFile(file, Path.GetFileNameWithoutExtension(file), Bass.ChannelGetLength(stream)));
                }

                Bass.Free();
            }
        }

        internal void LoadToList(ListBox listbox)
        {
            foreach (var data in Files)
            {
                if (!listbox.Items.Contains(data))
                {
                    listbox.Items.Add(data.FileName);
                }
            }

            listbox.DataContext = Files;
        }
    }
}
