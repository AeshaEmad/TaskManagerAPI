using TaskManagerAPI.DTOs.Tasks;
using TaskManagerAPI.Models;
using TaskManagerAPI.Repositories.Interfaces;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId);
            return tasks.Select(MapToResponseDto);
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int taskId, int userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(taskId, userId);
            return task == null ? null : MapToResponseDto(task);
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto createTaskDto, int userId)
        {
            var task = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Priority = createTaskDto.Priority,
                DueDate = createTaskDto.DueDate,
                Status = Models.TaskStatus.Pending,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _taskRepository.CreateAsync(task);
            await _taskRepository.SaveChangesAsync();

            return MapToResponseDto(task);
        }

        public async Task<TaskResponseDto?> UpdateTaskAsync(int taskId, UpdateTaskDto updateTaskDto, int userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(taskId, userId);
            if (task == null) return null;

            task.Title = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;
            task.Status = updateTaskDto.Status;
            task.Priority = updateTaskDto.Priority;
            task.DueDate = updateTaskDto.DueDate;

            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();

            return MapToResponseDto(task);
        }

        public async Task<bool> DeleteTaskAsync(int taskId, int userId)
        {
            var deleted = await _taskRepository.DeleteAsync(taskId, userId);
            if (!deleted) return false;

            await _taskRepository.SaveChangesAsync();
            return true;
        }

        private static TaskResponseDto MapToResponseDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UserId = task.UserId
            };
        }
    }
}
