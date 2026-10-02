using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(int userId);
        Task<TaskItem?> GetByIdAndUserIdAsync(int taskId, int userId);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<TaskItem> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int taskId, int userId);
        Task SaveChangesAsync();
    }
}
