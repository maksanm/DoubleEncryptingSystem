using Decoder.Core.Entities;
using Decryptor.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Decoder.Infrastructure.Persistance
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Message> Message { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public void Migrate()
        {
            this.Database.Migrate();
        }
    }
}
