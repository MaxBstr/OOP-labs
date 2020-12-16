using System;
using System.Collections.Generic;

namespace Backups.Storage
{
    public class SeparatedStorage : IStorage
    {
        readonly List<File> _filesInPoint = new List<File>();
        public int Size { get; set; }
        
        public File GetFileByName(string name)
        {
            return _filesInPoint.Find(f => f._fileName == name);
        }

        public void AddFiles(IEnumerable<File> files)
        {
            foreach (var f in files)
            {
                var copyFile = new File(f._fileName, f._size); //copy
                _filesInPoint.Add(copyFile);
                Size += copyFile._size;
            }
        }

        public void GetInfo()
        {
            foreach (var file in _filesInPoint)
            {
                Console.WriteLine($"{file._fileName} : {file._size}");
            }
        }
    }
}