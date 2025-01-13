using LinkApp.Data.Entities;

namespace LinkApp.Data.Abstractions
{
    public interface ILinksRepository
    {
        public Task Create(string token, string longLink);
        public Task<List<LinkEntity>> GetAll();
        public Task<string?> GetByToken(string token);
        public Task<string> Update(string token, string longLink);
        public Task<string> Delete(string token);
    }
}
