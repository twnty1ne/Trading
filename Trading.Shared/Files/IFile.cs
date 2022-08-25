using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Shared.Files
{
    public interface IFile
    {
        public string Path { get; }
        public string Text { get; }
        void SaveImmutable();
    }
}
