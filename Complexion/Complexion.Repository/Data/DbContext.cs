using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Data
{
    public class SqlDbContext : IDbContext, IAsyncDisposable
    {
        private readonly SqlConnection _connection;

        public SqlDbContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? parameters = null)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            return await _connection.ExecuteAsync(sql, parameters);
        }

        public async ValueTask DisposeAsync()
        {
            await _connection.DisposeAsync();
        }
    }
}