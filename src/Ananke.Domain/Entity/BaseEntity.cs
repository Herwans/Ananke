using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ananke.Domain.Entity
{
    public record BaseEntity
    {
        public int? Id { get; set; }
    }
}