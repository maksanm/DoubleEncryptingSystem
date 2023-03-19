using Decoder.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Decryptor.Core.Services.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Message> Message { get; set; }
        void Migrate();
    }
}
