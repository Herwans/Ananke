using Ananke.Domain.Entity;
using Newtonsoft.Json;

namespace Ananke.Infrastructure.Repository.Json
{
    public class ExtensionRepository : IExtensionRepository
    {
        private const string FILE = @"H:\Extension.json";
        private readonly List<Extension> _extensions;

        public ExtensionRepository()
        {
            if (!File.Exists(FILE))
            {
                _extensions = [];
            }
            else
            {
                _extensions = JsonConvert.DeserializeObject<List<Extension>>(File.ReadAllText(FILE));
            }
        }

        private void Save()
        {
            File.WriteAllText(FILE, JsonConvert.SerializeObject(_extensions));
        }

        public void Add(string extension)
        {
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