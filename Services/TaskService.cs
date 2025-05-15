using Task_Manager_API.Models;
using Task_Manager_API.Repository;

namespace Task_Manager_API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            return await _repository.CreateAsync(task);
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskItem task)
        {
            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
                return false;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;

            await _repository.UpdateAsync(existingTask);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
                return false;

            await _repository.DeleteAsync(existingTask);
            return true;
        }
    }
}
