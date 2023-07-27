namespace TaskMaster.Test
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _controller = new TasksController(_mockTaskRepository.Object);
        }

        [Fact]
        public async void GetTasks_ReturnsAllTasks()
        {
            // Arrange
            var tasks = new List<TaskModel>
            {
                new TaskModel { Id = 1, Title = "Task 1" },
                new TaskModel { Id = 2, Title = "Task 2" }
            };

            _mockTaskRepository.Setup(repo => repo.GetTasks()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetTasks();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<List<TaskModel>>(actionResult.Value);
            Assert.Equal(tasks.Count, model.Count);
        }

        [Fact]
        public async void GetTaskById_ReturnsTask()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            _mockTaskRepository.Setup(repo => repo.GetTaskById(task.Id)).ReturnsAsync(task);

            // Act
            var result = await _controller.GetTaskById(task.Id);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<TaskModel>(actionResult.Value);
            Assert.Equal(task, model);
        }

        [Fact]
        public async void GetTaskById_ReturnsNotFound()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            _mockTaskRepository.Setup(repo => repo.GetTaskById(task.Id)).Throws<KeyNotFoundException>();

            // Act
            Exception ex = await Record.ExceptionAsync(() => _controller.GetTaskById(task.Id));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<KeyNotFoundException>(ex);
        }

        [Fact]
        public async void CreateTask_ReturnsCreatedTask()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            // Act
            var result = await _controller.CreateTask(task);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<TaskModel>(actionResult.Value);
            Assert.Equal(task, model);
        }

        [Fact]
        public async void UpdateTask_ReturnsNoContent()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            // Act
            var result = await _controller.UpdateTask(task);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void UpdateTask_ReturnsNotFound()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            _mockTaskRepository.Setup(repo => repo.UpdateTask(task)).Throws<KeyNotFoundException>();

            // Act
            Exception ex = await Record.ExceptionAsync(() => _controller.UpdateTask(task));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<KeyNotFoundException>(ex);
        }

        [Fact]
        public async void DeleteTaskById_ReturnsNoContent()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            // Act
            var result = await _controller.DeleteTaskById(task.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteTaskById_ReturnsNotFound()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Task 1", IsComplete = false };

            _mockTaskRepository.Setup(repo => repo.DeleteTaskById(task.Id)).Throws<KeyNotFoundException>();

            // Act
            Exception ex = await Record.ExceptionAsync(() => _controller.DeleteTaskById(task.Id));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<KeyNotFoundException>(ex);
        }
    }
}
