using Ananke.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Repository.Database
{
    public class ExtensionRepository(AnankeContext context) : IExtensionRepository
    {
        private readonly AnankeContext _context = context;

        public async Task AddAsync(string extension, CancellationToken cancellationToken = default)
        {
            await _context.Extensions.AddAsync(new() { Name = extension.ToLower() }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Extension?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Extensions.FirstAsync(ext => ext.Name == name);
        }
    }
}