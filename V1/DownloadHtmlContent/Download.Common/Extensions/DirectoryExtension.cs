using System.IO;

namespace Download.Common.Extensions
{
    public  class DirectoryExtension
    {
        public  DirectoryInfo Create(string directoryPath)
        {
            return IsExits(directoryPath) ? null : Directory.CreateDirectory(directoryPath);
        }

        public  bool IsExits(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public  bool Delete(string path)
        {
            if(!IsExits(path))
                return false;

            var directoryInfo = new DirectoryInfo(path);

            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (var dir in directoryInfo.GetDirectories())
            {
                dir.Delete(true);
            }
            return true;
        }

        public  FileInfo[] GetFilesInfo(string directoryPath)
        {
            if (!IsExits(directoryPath))
                return null;

            var directoryInfo = new DirectoryInfo(directoryPath);

            return directoryInfo.GetFiles();
        }

        public  string[] GetFiles(string directoryPath)
        {
            if (!IsExits(directoryPath))
                return null;

            return Directory.GetFiles(directoryPath);
        }

        public  DirectoryInfo[] GetAllDirectories(string path)
        {
            if (!IsExits(path))
                return null;

            var directoryInfo = new DirectoryInfo(path);

            return directoryInfo.GetDirectories();
        }
    }
}
