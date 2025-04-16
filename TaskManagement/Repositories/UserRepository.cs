using Dapper;
using Microsoft.Data.SqlClient;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Users (UserName, Email, CreatedAt)
                        VALUES (@UserName, @Email, @CreatedAt);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"UPDATE Users SET UserName = @UserName, Email = @Email WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, user) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id }) > 0;
        }
    }
}
