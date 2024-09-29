using Ananke.Infrastructure.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ananke.Infrastructure.Persistence.EFCore.Repositories
{
    public abstract class BaseRepository(AnankeContext context)
    {
        protected readonly AnankeContext _context = context;
    }
}