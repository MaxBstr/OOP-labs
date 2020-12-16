using System.Collections.Generic;
using Backups.Points;

namespace Backups.LimitAlgorithms
{
    public interface ILimitAlgorithm
    {
        void RunCleaner(int countPointsToDelete);
        int GetPointsCountToDelete(List<IPoint> points);
    }
}