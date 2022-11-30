using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Trading.Shared.Files
{
    public class LocalFile : IFile
    {
        public LocalFile(string path, string text)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Path { get; private set; }
        public string Text { get; private set; }

        public void SaveImmutable() 
        {
            if (!File.Exists(Path)) 
            {
                using var sw = File.CreateText(Path);
                sw.Write(Text);
            }
            
        }
    }
}
