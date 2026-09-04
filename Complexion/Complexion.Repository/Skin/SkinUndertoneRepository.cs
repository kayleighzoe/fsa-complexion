using System.Data.Common;
using Complexion.Models.Skin;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Skin
{
    public class SkinUndertoneRepository : ISkinUndertoneRepository
    {
        private readonly SqlConnection _connection;

        public SkinUndertoneRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<SkinUndertone>> GetAllUndertonesAsync()
        {
            var sql = @"SELECT 
                            UndertoneId, 
                            Name 
                        FROM skin.Undertone";

            return await _connection.QueryAsync<SkinUndertone>(sql);
        }
    }
}