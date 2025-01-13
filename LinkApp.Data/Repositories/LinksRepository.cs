using LinkApp.Data.Abstractions;
using LinkApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkApp.Data.Repositories
{
    public class LinksRepository(LinkAppDbContext dbContext) : ILinksRepository
    {
        private readonly LinkAppDbContext _dbContext = dbContext;

        public async Task Create(string token, string longLink)
        {
            var link = new LinkEntity()
            {
                Token = token,
                LongLink = longLink
            };

            await _dbContext.AddAsync(link);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LinkEntity>> GetAll()
        {
            var entities = await _dbContext.Links
                .AsNoTracking()
                .ToListAsync();

            return entities;
        }

        public async Task<string?> GetByToken(string token)
        {
            var entity = await _dbContext.Links
                .Where(l => l.Token == token)
                .FirstOrDefaultAsync();
         
            return entity?.LongLink;
        }

        public async Task<string> Update(string token, string longLink)
        {
            await _dbContext.Links
                .Where(l => l.Token == token)
                .ExecuteUpdateAsync(l => l
                    .SetProperty(p => p.Token, token)
                    .SetProperty(p => p.LongLink, longLink));

            return token;
        }

        public async Task<string> Delete(string token)
        {
            await _dbContext.Links
                .Where(l => l.Token == token)
                .ExecuteDeleteAsync();

            return token;
        }
    }
}
