using System;
using System.Collections.Generic;
using Backups.LimitAlgorithms;
using Backups.Storage;

namespace Backups
{
    public static class BackupManager
    {
        private static readonly List<Backup> Backups = new List<Backup>();

        private static Backup FindBackup(int id)
        {
            var bc = Backups.Find(b => b.BackupId == id);
            if (bc == null)
                throw new Exception($"Backup with {id} id was not found");

            return bc;
        }
        
        public static void MakeBackup<T>() where T : IStorage
        {
            Backups.Add(new Backup(Backups.Count + 1, typeof(T)));
        }

        public static void AddFilesToBackupById(int backupId, params File[] files)
        {
            var bc = FindBackup(backupId);
            bc.AddFiles(files);
        }

        public static void CreateFullRestorePointInBackupById(int backupId)
        {
            var bc = FindBackup(backupId);
            bc.CreateFullRestorePoint();
        }

        public static void EditFileInBackupById(int backupId, string fileName, int newSize)
        {
            var bc = FindBackup(backupId);
            bc.EditFile(fileName, newSize);
        }

        public static void CreateIncrementalRestorePointInBackupById(int backupId)
        {
            var bc = FindBackup(backupId);
            bc.CreateIncrementalPoint();
        }

        public static void DeleteFileFromBackupById(int backupId, string fileName)
        {
            var bc = FindBackup(backupId);
            bc.RemoveFile(fileName);
        }
        
        public static void UseLimitAlgorithmInBackupById(int backupId, ILimitAlgorithm algo)
        {
            var bc = FindBackup(backupId);
            bc.RemovePoints(algo);
        }

        public static void GetInfoAboutPointsInBackupById(int backupId)
        {
            var bc = FindBackup(backupId);
            bc.GetPointsInfo();
        }

        public static void UseHybridAlgorithmInBackupById(int backupId, bool isMost, params ILimitAlgorithm[] algos)
        {
            var bc = FindBackup(backupId);
            bc.UseHybrid(isMost, algos);
        }

        public static void GetInfoAboutPointInBackupById(int backupId, int pointId)
        {
            var bc = FindBackup(backupId);
            bc.GetInfoAboutPointById(pointId);
        }

    }
}