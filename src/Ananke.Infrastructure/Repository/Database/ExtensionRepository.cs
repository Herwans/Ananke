using Ananke.Domain.Entity;

namespace Ananke.Infrastructure.Repository.Database
{
    public class ExtensionRepository(AnankeContext context) : IExtensionRepository
    {
        private readonly AnankeContext _context = context;

        public void Add(string extension)
        {
            _context.Extensions.Add(new() { Name = extension.ToLower() });
        }

        public Extension? GetByName(string name)
        {
            return _context.Extensions.First(ext => ext.Name == name);
        }
    }
}