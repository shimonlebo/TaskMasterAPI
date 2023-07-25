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
        public async void Get_ReturnsAllTasks()
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
        public async void Get_ReturnsTaskById()
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
    }
}
