using Backups.LimitAlgorithms;
using Backups.Storage;

namespace Backups
{
    internal static class Program
    {
        private static void Main()
        {
            //1 case
            BackupManager.MakeBackup<JointStorage>();
            BackupManager.AddFilesToBackupById(1, new File("file1", 200),
                                                                    new File("file2", 200));
            BackupManager.CreateFullRestorePointInBackupById(1);
            BackupManager.GetInfoAboutPointInBackupById(1, 1);
            BackupManager.CreateFullRestorePointInBackupById(1);
            BackupManager.UseLimitAlgorithmInBackupById(1, new LimitAlgoByCount(1));
            BackupManager.GetInfoAboutPointsInBackupById(1);
            
            //2 case
            BackupManager.MakeBackup<SeparatedStorage>();
            BackupManager.AddFilesToBackupById(2, new File("file1", 100),
                                                                    new File("file2", 100));
            BackupManager.CreateFullRestorePointInBackupById(2);
            BackupManager.GetInfoAboutPointInBackupById(2, 1);
            BackupManager.CreateFullRestorePointInBackupById(2);
            BackupManager.UseLimitAlgorithmInBackupById(2, new LimitAlgoBySize(150));
            BackupManager.GetInfoAboutPointsInBackupById(2);
            
            //3 case
            BackupManager.MakeBackup<SeparatedStorage>();
            BackupManager.AddFilesToBackupById(3, new File("file1", 300),
                                                                    new File("file2", 300));
            BackupManager.CreateFullRestorePointInBackupById(3);
            BackupManager.GetInfoAboutPointInBackupById(3, 1);
            BackupManager.CreateFullRestorePointInBackupById(3);
            //hybrid testing
            //first test: the greatest count (will delete 1 point) - flag: true;
            //second test: the lowest count (will not delete points) - flag: false;
            BackupManager.UseHybridAlgorithmInBackupById(3, false, new LimitAlgoByCount(2),
                                                                                    new LimitAlgoBySize(600));
            BackupManager.GetInfoAboutPointsInBackupById(3);
        }
    }
}