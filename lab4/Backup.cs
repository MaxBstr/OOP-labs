using System;
using System.Collections.Generic;
using System.Linq;
using Backups.LimitAlgorithms;
using Backups.Points;
using Backups.Storage;

namespace Backups
{
    public class Backup
    {
        private DateTime _backupCreationTime;
        public readonly int BackupId;
        private readonly List<File> _backupingFiles = new List<File>();
        private readonly List<IPoint> _points = new List<IPoint>();
        private int _backupSize;
        private readonly Type _storageAlgorithmType;

        public Backup(int id, Type storageType)
        {
            BackupId = id;
            _backupCreationTime = DateTime.Now;
            _storageAlgorithmType = storageType;
        }

        public void AddFiles(params File[] files)
        {
            foreach (var f in files)
            {
                _backupingFiles.Add(f);
            }
        }

        public void EditFile(string fileName, int size)
        {
            var file = _backupingFiles.Find(f => f._fileName == fileName);
            if (file == null)
                throw new Exception("File was not found");

            file.ChangeSize(size);
        }

        public void CreateFullRestorePoint()
        {
            _points.Add(new FullRestorePoint(_points.Count + 1, _storageAlgorithmType, _backupingFiles));
            _backupSize = _points[^1].Storage.Size;
        }

        public void CreateIncrementalPoint()
        {
            if (_points.Count == 0)
                throw new Exception("You can`t create inc restore point without full restore point");

            _points.Add(new IncrementalRestorePoint(_points.Count + 1, _storageAlgorithmType, _points, _backupingFiles));
            _backupSize += _points[^1].Storage.Size;
        }

        public void RemoveFile(string name)
        {
            var file = _backupingFiles.Find(f => f._fileName == name);
            if (file == null)
                throw new Exception($"File {name} was not found");
            _backupingFiles.Remove(file);
        }

        public void RemovePoints(ILimitAlgorithm algo)
        {
            algo.RunCleaner(algo.GetPointsCountToDelete(_points));
        }

        public void UseHybrid(bool isMost, params ILimitAlgorithm[] algos)
        {
            var countPointByAlgo = new Dictionary<int, int>(); // key - algo, value - countRemovingPoints
            //считаем - сколько точек предложил удалить каждый из алгоритмов
            for (var i = 0; i < algos.Length; ++i)
            {
                var count = algos[i].GetPointsCountToDelete(_points);
                countPointByAlgo[i] = count;
            }

            var finalCount = 0;
            var algoId = -1;
            //isMost == true - наибольшее кол-во точек, false - наименьшее
            if (isMost) //наибольшее кол-во точек
            {
                var curCount = int.MinValue;
                foreach (var algoKey in countPointByAlgo.Keys.Where(
                    algoKey => countPointByAlgo[algoKey] > curCount))
                {
                    curCount = countPointByAlgo[algoKey];
                    algoId = algoKey;
                }
                finalCount = curCount;
            } //наименьшее кол-во точек
            else
            {
                var curCount = int.MaxValue;
                foreach (var algoKey in countPointByAlgo.Keys.Where(
                    algoKey => countPointByAlgo[algoKey] < curCount))
                {
                    curCount = countPointByAlgo[algoKey];
                    algoId = algoKey;
                }
                finalCount = curCount;
            }
            
            algos[algoId].RunCleaner(finalCount);
        }

        public void GetPointsInfo()
        {
            if (_points.Count == 0)
                throw new Exception("No points were found!");
            
            foreach(var point in _points)
                Console.WriteLine($"{point.GetType()} : {point.PointId}");
        }

        public void GetInfoAboutPointById(int id)
        {
            var point = _points.Find(p => p.PointId == id);
            if (point == null)
                throw new Exception($"Unknown point with id: {id}");
            
            point.GetInfo();
        }
        
    }
}