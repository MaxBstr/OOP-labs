using System;
using System.Collections.Generic;

namespace Backups.Storage
{
    public class JointStorage : IStorage
    {
        readonly Dictionary<string, int> _filesInPoint = new Dictionary<string, int>();
        public int Size { get; set; }
 
        public File GetFileByName(string name)
        {
            foreach (var (key, value) in _filesInPoint)
            {
                if (key != name) continue;
                return new File(key, value);
            }

            return null;
        }

        public void AddFiles(IEnumerable<File> files)
        {
            foreach (var f in files)
            {
                var copyFile = new File(f._fileName, f._size); // copy
                _filesInPoint[copyFile._fileName] = copyFile._size;
                Size += copyFile._size;
            }
        }

        public void GetInfo()
        {
            foreach (var (key, value) in _filesInPoint)
            {
                Console.WriteLine($"{key} : {value}");
            }
        }
    }
}