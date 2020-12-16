using System.Collections.Generic;

namespace Backups.Storage
{
    public interface IStorage
    {
        void AddFiles(IEnumerable<File> files);
        int Size { get; set; }
        File GetFileByName(string name);

        void GetInfo();
    }
}