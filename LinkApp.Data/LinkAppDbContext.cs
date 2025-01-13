using LinkApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkApp.Data
{
    public class LinkAppDbContext(DbContextOptions<LinkAppDbContext> options)
        : DbContext(options)
    {
        public required DbSet<LinkEntity> Links { get; set; }
    }
}
