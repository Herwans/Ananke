using Ananke.Domain.Entity.Items;
using Ananke.Infrastructure.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ananke.Infrastructure.Persistence.EFCore.Repositories
{
    public class ExtensionRepository(AnankeContext context) : BaseRepository(context), IExtensionRepository
    {
        public async Task AddAsync(string extension, CancellationToken cancellationToken = default)
        {
            await _context.Extensions.AddAsync(new() { Name = extension.ToLower() }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Extension?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Extensions.FirstAsync(ext => ext.Name == name, cancellationToken);
        }
    }
}