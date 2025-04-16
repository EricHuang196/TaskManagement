using TaskManagement.Models;

namespace TaskManagement.Services
{
    public interface ITaskService
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
