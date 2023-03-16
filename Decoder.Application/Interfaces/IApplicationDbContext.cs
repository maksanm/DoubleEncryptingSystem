using Decoder.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Decoder.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Message> Message { get; set; }
        void Migrate();
    }
}
