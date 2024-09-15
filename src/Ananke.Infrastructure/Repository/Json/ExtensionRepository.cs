using Ananke.Domain.Entity;
using Newtonsoft.Json;

namespace Ananke.Infrastructure.Repository.Json
{
    public class ExtensionRepository : IExtensionRepository
    {
        private readonly string _file;
        private readonly List<Extension> _extensions;

        public ExtensionRepository(string file)
        {
            _file = file;
            if (!File.Exists(_file))
            {
                _extensions = [];
            }
            else
            {
                _extensions = JsonConvert.DeserializeObject<List<Extension>>(File.ReadAllText(_file));
            }
        }

        private void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_file));
            File.WriteAllText(_file, JsonConvert.SerializeObject(_extensions));
        }

        public void Add(string extension)
        {
            if (_extensions.Find(ext => ext.Name == extension) != null) return;
            int lastId = _extensions.Max(extension => extension.Id) ?? 0;
            Extension ext = new()
            {
                Id = lastId + 1,
                Name = extension
            };
            _extensions.Add(ext);
            Save();
        }

        public Extension? GetByName(string name)
        {
            return _extensions.Find(x => x.Name == name);
        }
    }
}