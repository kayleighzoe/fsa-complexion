using Complexion.Models.Skin;
using Complexion.Repository.Data;

namespace Complexion.Repository.Skin
{
    public class SkinUndertoneRepository : ISkinUndertoneRepository
    {
        private readonly IDbContext _dbContext;

        public SkinUndertoneRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SkinUndertone>> GetAllUndertonesAsync()
        {
            const string sql = @"SELECT 
                            UndertoneId, 
                            Name 
                        FROM skin.Undertone";

            return await _dbContext.QueryAsync<SkinUndertone>(sql);
        }
    }
}