using SalesProcessor.Helpers;
using System;
using System.IO;

namespace SalesProcessor.Core.Handlers
{
    public class FileHandler
    {
        private readonly string _directory;
        private readonly string _folder;
        private readonly string _folderError;
        private readonly string _termination;

        public FileHandler()
        {
            _directory = Environment.CurrentDirectory.Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "");
            _folder = $@"{_directory}\data";
            _folderError = $@"{_folder}\error";
            _termination = $"-ProcessedWithError-in-{DateTimeHelper.DateTimeBrazil("yyyy-MM-dd-HHmmss")}.txt";

            CreateDirectory(_folder);
            CreateDirectory(_folderError);
        }

        public string GetDataDirectory()
        {
            return _folder;
        }

        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public FileInfo[] GetFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            return directory.GetFiles();
        }

        public StreamReader ReadFile(FileInfo file)
        {
            try
            {
                return file.OpenText();
            }
            catch (Exception ex)
            {
                var path = $@"{_folderError}\{file.Name.Replace(".txt", _termination)}";

                file.MoveTo(path);
                file.Delete();

                throw ex;
            }
        }
    }
}
