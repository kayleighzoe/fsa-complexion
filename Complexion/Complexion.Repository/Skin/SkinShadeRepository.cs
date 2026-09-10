using System.Data.Common;
using Complexion.Models.Skin;
using Complexion.Repository.Data;
using Dapper;
using Microsoft.Data.SqlClient;

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
            var sql = @"SELECT 
                            ShadeId, 
                            Name 
                        FROM skin.Shade";

            return await _dbContext.QueryAsync<SkinShade>(sql);
        }
    }
}