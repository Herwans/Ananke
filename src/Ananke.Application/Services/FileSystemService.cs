using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Directory.GetDirectories(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}