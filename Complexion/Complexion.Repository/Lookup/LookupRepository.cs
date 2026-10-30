using Complexion.Models.Products;
using Complexion.Repository.Data;

namespace Complexion.Repository.Lookups
{
    public class LookupRepository : ILookupRepository
    {
        private readonly IDbContext _dbContext;

        public LookupRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShareLookupIds> GetShareLookupIdsAsync(ShareRecommendationDto dto)
        {
            const string sql = @"SELECT
                                    (SELECT TOP 1 CategoryId FROM catalogue.Category WHERE Name = @CategoryName) AS CategoryId,
                                    (SELECT TOP 1 PriceTierId FROM config.PriceTier WHERE Name = @PriceTierName) AS PriceTierId,
                                    (SELECT TOP 1 ShadeId FROM skin.Shade WHERE Name = @MatchedShadeName) AS ShadeId,
                                    (SELECT TOP 1 UndertoneId FROM skin.Undertone WHERE Name = @MatchedUndertoneName) AS UndertoneId";

            return await _dbContext.QuerySingleAsync<ShareLookupIds>(sql, new
            {
                dto.CategoryName,
                dto.PriceTierName,
                dto.MatchedShadeName,
                dto.MatchedUndertoneName
            });
        }
    }
}