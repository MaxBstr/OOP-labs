using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backups.Storage;

namespace Backups.Points
{
    public class IncrementalRestorePoint : IPoint
    {
        public int PointId { get; set; }
        public DateTime PointCreationTime { get; set; }
        public IStorage Storage { get; }

        public IncrementalRestorePoint(int id, Type storage, List<IPoint> previousPoints, List<File> newFiles)
        {
            PointId = id;
            PointCreationTime = DateTime.Now;
            
            if (storage == typeof(JointStorage))
                Storage = new JointStorage();
            else
                Storage = new SeparatedStorage();
            
            var deltaFiles = new List<File>();
            var indexOfLastFullPoint = previousPoints.FindLastIndex(p => p.GetType() == typeof(FullRestorePoint));
            
            
            foreach (var file in newFiles)
            {
                var sumDelta = 0;
                //бегаем от последней full точки до последней inc точки
                for (var i = indexOfLastFullPoint; i < previousPoints.Count; ++i)
                {
                    var curPoint = previousPoints[i];
                    var curFile = curPoint.Storage.GetFileByName(file._fileName);
                    if (curFile == null)
                        throw new Exception("Can`t create inc restore point.\n" +
                                            "You added file and didn`t create full restore point");

                    sumDelta += curFile._size;
                }

                deltaFiles.Add(sumDelta == file._size
                    ? new File(file._fileName, 0)
                    : new File(file._fileName, file._size - sumDelta));
            }
            
            Storage.AddFiles(deltaFiles);
        }

        public void GetInfo()
        {
            Storage.GetInfo();
        }
    }
}