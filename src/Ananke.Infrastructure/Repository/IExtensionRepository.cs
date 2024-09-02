using Ananke.Domain.Entity;

namespace Ananke.Infrastructure.Repository
{
    public interface IExtensionRepository
    {
        void Add(string extension);

        Extension? GetByName(string name);
    }
}