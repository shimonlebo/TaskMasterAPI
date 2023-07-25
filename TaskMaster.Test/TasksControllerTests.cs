using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.API.Controllers;
using TaskMaster.API.Models;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.Test
{
    public class TasksControllerTests
    {
        [Fact]
        public async void Get_ReturnsAllTasks()
        {
            // Arrange
            var mockTastRepository = new Mock<ITaskRepository>();

            var tasks = new List<TaskModel>
            {
                new TaskModel { Id = 1, Title = "Task 1" },
                new TaskModel { Id = 2, Title = "Task 2" }
            };

            mockTastRepository.Setup(repo => repo.GetTasks()).ReturnsAsync(tasks);

            var controller = new TasksController(mockTastRepository.Object);

            // Act
            var result = await controller.GetTasks();
            
            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<List<TaskModel>>(actionResult.Value);
            Assert.Equal(tasks.Count, model.Count);            
        }
    }
}
