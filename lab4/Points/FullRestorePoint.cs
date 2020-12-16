using System;
using System.Collections.Generic;
using Backups.Storage;

namespace Backups.Points
{
    public class FullRestorePoint : IPoint
    {
        public int PointId { get; set; }
        public DateTime PointCreationTime { get; set; }
        public IStorage Storage { get; }
        
        public FullRestorePoint(int id, Type storage, IEnumerable<File> files)
        {
            PointId = id;
            PointCreationTime = DateTime.Now;
            
            if (storage == typeof(JointStorage))
                Storage = new JointStorage();
            else
                Storage = new SeparatedStorage();
            
            Storage.AddFiles(files);
        }
        
        public void GetInfo()
        {
            Storage.GetInfo();
        }
    }
}