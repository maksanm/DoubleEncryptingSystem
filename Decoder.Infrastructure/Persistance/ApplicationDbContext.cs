using Decoder.Application.Interfaces;
using Decoder.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Decoder.Infrastructure.Persistance
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Message> Message { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    }
}
