using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Points;

namespace Backups.LimitAlgorithms
{
    public class LimitAlgoByDate : ILimitAlgorithm
    {
        private DateTime _dateLimit;
        private List<IPoint> _points = new List<IPoint>();

        public LimitAlgoByDate(DateTime dt)
        {
            _dateLimit = dt;
        }
        
        public int GetPointsCountToDelete(List<IPoint> points)
        {
            _points = points;
            var countPointsToDelete = points.Count(point => point.PointCreationTime < _dateLimit);

            //если нечего удалять
            return countPointsToDelete == 0 ? 0 : countPointsToDelete;
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