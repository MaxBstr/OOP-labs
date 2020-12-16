using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Points;

namespace Backups.LimitAlgorithms
{
    public class LimitAlgoBySize : ILimitAlgorithm
    {
        private readonly int _sizeLimit;
        private List<IPoint> _points = new List<IPoint>();

        public LimitAlgoBySize(int size)
        {
            _sizeLimit = size;
        }

        public int GetPointsCountToDelete(List<IPoint> points)
        {
            _points = points;
            var curPointsSize = 0;
            var countPointsToDelete = _points.Count;
            //если последняя точка фул и она сразу превышает лимит, то удаляем все точки до нее
            if (_points[^1].Storage.Size >= _sizeLimit && _points[^1].GetType() == typeof(FullRestorePoint))
                return countPointsToDelete - 1;
            
            //бегаем с конца, суммируем размеры точек до тех пор, пока не превышен лимит
            for (var i = _points.Count - 1; i >= 0; --i)
            {
                curPointsSize += _points[i].Storage.Size;
                if (curPointsSize <= _sizeLimit)
                {
                    countPointsToDelete--;
                }
                else
                    break;
            }
            //если нечего удалять
            if (countPointsToDelete == _points.Count || countPointsToDelete == 0)
                return 0;
            
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