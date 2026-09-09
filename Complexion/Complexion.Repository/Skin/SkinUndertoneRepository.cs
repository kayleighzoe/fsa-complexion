using System.Data.Common;
using Complexion.Models.Skin;
using Complexion.Repository.Data;
using Dapper;
using Microsoft.Data.SqlClient;

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
            var sql = @"SELECT 
                            UndertoneId, 
                            Name 
                        FROM skin.Undertone";

            return await _dbContext.QueryAsync<SkinUndertone>(sql);
        }
    }
}