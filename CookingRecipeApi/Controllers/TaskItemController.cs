using AutoMapper;
using AutoMapper.Configuration;
using CookingRecipeApi.Common;
using CookingRecipeApi.Controllers;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Request;
using CookingRecipeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CookingRecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : BaseApiController<TaskItemController>
    {
        private readonly TaskItemService _taskItemService;
        public TaskItemController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _taskItemService = new TaskItemService(apiConfig, databaseContext, mapper, webHost);
        }

        /// <summary>
        /// Get task list by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TaskList")]
        public MessageData TaskList(int type = 1, int perPage = 10, int currentPage = 1)
        {
            try
            {
                var res = _taskItemService.GetTaskList(UserId, perPage, currentPage, type);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get task detail
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TaskDetail")]
        public MessageData TaskDetail(int taskId)
        {
            try
            {
                var res = _taskItemService.TaskDetail(UserId, taskId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// User create task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateTask")]
        public MessageData CreateTask([FromForm]CreateTaskRequest request)
        {
            try
            {
                var res = _taskItemService.CreateTask(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// User update task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateTask")]
        public MessageData UpdateTask([FromForm] UpdateTaskRequest request)
        {
            try
            {
                var res = _taskItemService.UpdateTask(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteTask")]
        public MessageData DeleteTask(int taskId)
        {
            try
            {
                var res = _taskItemService.DeleteTask(UserId, taskId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ArchiveTask")]
        public MessageData ArchiveTask(int taskId)
        {
            try
            {
                var res = _taskItemService.ArchiveTask(UserId, taskId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
