using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Api.Models;

namespace Complexion.Api.Repositories;

public class SkinShadeRepository : ISkinShadeRepository
{
    private readonly string _connectionString;

    public SkinShadeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<SkinShade>> GetAllSkinShades()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<SkinShade>("SELECT * FROM skin.Shade");
    }
}