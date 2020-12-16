using System;
using System.Collections.Generic;
using Backups.Points;

namespace Backups.LimitAlgorithms
{
    public class LimitAlgoByCount : ILimitAlgorithm
    {
        private int _limitCount;
        private List<IPoint> _points = new List<IPoint>();

        public LimitAlgoByCount(int count)
        {
            _limitCount = count;
        }

        public int GetPointsCountToDelete(List<IPoint> points)
        {
            _points = points;
            if (_points.Count <= _limitCount)
                return 0;
            
            //в противном случае нужно удалить точки, ибо они превышают лимит
            var countPointsToDelete = _points.Count - _limitCount;
            return countPointsToDelete;
        }

        public void RunCleaner(int countPointsToDelete)
        {
            if (countPointsToDelete == 0)
                return;
            
            var lastFullPointIndex = _points.FindLastIndex(p => p.GetType() == typeof(FullRestorePoint));
            if (lastFullPointIndex == 0 && _points.Count != 1) //если только 1 фульная точка => она первая
            {
                Console.WriteLine("Warning! Count of points greater than limit");
                return;
            }

            var deletedPointsCount = 0;
            //в противном случае удаляем фульную точку и все инкрементальные от нее
            var pointsToDelete = new List<IPoint>();
            
            foreach (var point in _points)
            {
                if (point.GetType() == typeof(FullRestorePoint) && deletedPointsCount < countPointsToDelete)
                {
                    pointsToDelete.Add(point);
                    deletedPointsCount++;
                    continue;
                }

                if (point.GetType() == typeof(FullRestorePoint) && deletedPointsCount >= countPointsToDelete)
                    break;

                pointsToDelete.Add(point);
                deletedPointsCount++;
            }

            foreach (var point in pointsToDelete)
            {
                _points.Remove(point);
            }
        }
    }
}