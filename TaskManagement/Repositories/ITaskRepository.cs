using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<int> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);

        Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId, bool? isCompleted = null);

        Task<PagedResult<TaskItem>> GetByUserIdPagedAsync(int userId, bool? isCompleted, int page, int pageSize);
    }
}
