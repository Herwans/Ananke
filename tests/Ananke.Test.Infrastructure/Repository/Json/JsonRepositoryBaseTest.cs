using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ananke.Test.Infrastructure.Repository.Json
{
    public class JsonRepositoryBaseTest
    {
        protected static bool DeleteFile(string filename)
        {
            File.Delete(filename);
            return !File.Exists(filename);
        }

        protected static bool DeleteDirectory(string directory)
        {
            if (!Directory.Exists(directory)) return true;
            Directory.Delete(directory, true);
            return !Directory.Exists(directory);
        }
    }
}