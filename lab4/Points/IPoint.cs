using System;
using Backups.Storage;

namespace Backups.Points
{
    public interface IPoint
    {
        int PointId { get; set; }
        DateTime PointCreationTime { get; set; }
        IStorage Storage { get; }
        void GetInfo();
    }
}