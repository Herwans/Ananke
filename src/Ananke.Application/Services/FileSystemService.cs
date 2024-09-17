namespace Ananke.Application.Services
{
    public interface IFileSystemService
    {
        public string[] GetFiles(string path);

        public string[] GetDirectories(string path);

        public string[] GetContent(string path);
    }

    public class FileSystemService : IFileSystemService
    {
        public string[] GetContent(string path)
        {
            return [.. GetDirectories(path), .. GetFiles(path)];
        }

        public string[] GetDirectories(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {
                return [];
            }
        }

        public string[] GetFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch
            {
                return [];
            }
        }
    }
}