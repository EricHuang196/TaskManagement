using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<IEnumerable<TaskItem>> GetAllAsync() => _taskRepository.GetAllAsync();

        public Task<TaskItem?> GetByIdAsync(int id) => _taskRepository.GetByIdAsync(id);

        public Task<int> CreateAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.Now;
            return _taskRepository.CreateAsync(task);
        }

        public Task<bool> UpdateAsync(TaskItem task) => _taskRepository.UpdateAsync(task);

        public Task<bool> DeleteAsync(int id) => _taskRepository.DeleteAsync(id);

        public Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId)
        {
            return _taskRepository.GetByUserIdAsync(userId);
        }

        public Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId, bool? isCompleted = null)
        {
            return _taskRepository.GetByUserIdAsync(userId, isCompleted);
        }

        public Task<PagedResult<TaskItem>> GetByUserIdPagedAsync(int userId, bool? isCompleted, int page, int pageSize)
        {
            return _taskRepository.GetByUserIdPagedAsync(userId, isCompleted, page, pageSize);
        }
    }
}
