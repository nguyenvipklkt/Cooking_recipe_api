using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using CookingRecipeApi.Utility;
using System.Linq.Expressions;

namespace CookingRecipeApi.Services
{
    public class TaskItemService
    {
        private readonly UserRepository _userRepository;
        private readonly TaskItemRepository _taskItemRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public TaskItemService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _taskItemRepository = new TaskItemRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// Get task list by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetTaskList(int userId, int perPage, int currentPage, int type)
        {
            try
            {
                var query = _taskItemRepository.FindByCondition(row => row.UserId == userId);
                var dateNow = DateTime.UtcNow;
                switch (type)
                {
                    case 1:
                        var startDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);
                        var endDate = startDate.AddDays(1);
                        query = query.Where(row => row.StartDate >= startDate && row.StartDate <= endDate);
                        break;
                    case 2:
                        startDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day).AddDays(1);
                        endDate = startDate.AddDays(1);
                        query = query.Where(row => row.StartDate >= startDate && row.StartDate <= endDate);
                        break;
                    case 3:
                        startDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);
                        endDate = startDate.AddDays(7);
                        query = query.Where(row => row.StartDate >= startDate && row.StartDate <= endDate);
                        break;
                    default:
                        break;
                }
                return new Pagination<TaskItem>(query, perPage, currentPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// User create task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public object CreateTask(int userId, CreateTaskRequest request)
        {
            try
            {
                var newTask = _mapper.Map<TaskItem>(request);
                newTask.Image = "";
                if (request.Image != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\tasks\\images\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    newTask.Image = "tasks/images/" + date + request.Image.FileName;
                }

                newTask.UserId = userId;
                _taskItemRepository.Create(newTask);
                _taskItemRepository.SaveChange();
                return newTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public object UpdateTask(int userId, UpdateTaskRequest request)
        {
            try
            {
                var task = _taskItemRepository.FindByCondition(row => row.Id == request.Id && userId == row.UserId).FirstOrDefault();

                if (task == null)
                {
                    throw new ValidateError(1001, "Task dont exist!");
                }

                if (request.Image != null && request.Image.FileName != task.Image)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\tasks\\images\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    task.Image = "tasks/images/" + date + request.Image.FileName;
                }
                task.Title = request.Title;
                task.Content = request.Content;
                task.Location = request.Location;
                task.Position = request.Position;
                task.StartDate = request.StartDate;
                task.EndDate = request.EndDate;
                task.UpdatedDate = DateTime.UtcNow;

                _taskItemRepository.UpdateByEntity(task);
                _taskItemRepository.SaveChange();
                return task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get task detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public object TaskDetail(int userId, int taskId)
        {
            try
            {
                return _taskItemRepository.FindByCondition(row => row.Id == taskId && userId == row.UserId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public object DeleteTask(int userId, int taskId)
        {
            try
            {
                var task = _taskItemRepository.FindByCondition(row => row.Id == taskId && userId == row.UserId).FirstOrDefault();
                if (task == null)
                {
                    throw new ValidateError(1001, "Task dont exist!");
                }
                _taskItemRepository.DeleteByEntity(task);
                _taskItemRepository.SaveChange();
                return task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Archive task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        /// <exception cref="ValidateError"></exception>
        public object ArchiveTask(int userId, int taskId)
        {
            try
            {
                var task = _taskItemRepository.FindByCondition(row => row.Id == taskId && userId == row.UserId).FirstOrDefault();
                if (task == null)
                {
                    throw new ValidateError(1001, "Task dont exist!");
                }

                task.Status = 2;
                task.UpdatedDate = DateTime.UtcNow;
                _taskItemRepository.UpdateByEntity(task);
                _taskItemRepository.SaveChange();
                return task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
