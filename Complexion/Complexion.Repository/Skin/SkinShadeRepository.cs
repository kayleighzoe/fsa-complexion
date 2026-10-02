using Complexion.Models.Skin;
using Complexion.Repository.Data;

namespace Complexion.Repository.Skin
{
    public class SkinShadeRepository : ISkinShadeRepository
    {
        private readonly IDbContext _dbContext;

        public SkinShadeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SkinShade>> GetAllSkinShadesAsync()
        {
            const string sql = @"SELECT 
                            ShadeId, 
                            Name 
                        FROM skin.Shade";

            return await _dbContext.QueryAsync<SkinShade>(sql);
        }
    }
}