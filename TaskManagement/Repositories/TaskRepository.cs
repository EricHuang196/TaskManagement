using Dapper;
using Microsoft.Data.SqlClient;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public TaskRepository(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            using var conn = new SqlConnection(_conn);
            return await conn.QueryAsync<TaskItem>("SELECT * FROM Tasks");
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_conn);
            return await conn.QueryFirstOrDefaultAsync<TaskItem>(
                "SELECT * FROM Tasks WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(TaskItem task)
        {
            using var conn = new SqlConnection(_conn);
            var sql = @"
                INSERT INTO Tasks (UserId, Title, Description, IsCompleted, DueDate, CreatedAt)
                VALUES (@UserId, @Title, @Description, @IsCompleted, @DueDate, @CreatedAt);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            return await conn.ExecuteScalarAsync<int>(sql, task);
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            using var conn = new SqlConnection(_conn);
            var sql = @"
                UPDATE Tasks SET
                    UserId = @UserId,
                    Title = @Title,
                    Description = @Description,
                    IsCompleted = @IsCompleted,
                    DueDate = @DueDate
                WHERE Id = @Id";
            return await conn.ExecuteAsync(sql, task) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_conn);
            return await conn.ExecuteAsync("DELETE FROM Tasks WHERE Id = @Id", new { Id = id }) > 0;
        }
    }
}
