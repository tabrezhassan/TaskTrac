using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using TaskTrac.API.Controllers;
using Microsoft.AspNetCore.Identity;
using static NUnit.Framework.Constraints.Tolerance;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using TaskTrac.API.DTO;

namespace TaskTrac.Tests
{
    [TestFixture]
    public class TasksTests
    {
        private ITaskService _taskService;
        private ISubTaskService _subTaskService;
        private UserManager<Users> _userManager;

        private TasksController CreateController(ITaskService taskService, ISubTaskService subTaskService, UserManager<Users> userManager)
        {
            return new TasksController(taskService, subTaskService,userManager);
        }

        private List<Tasks> MockTasks()
        {
            var tasks = new List<Tasks>
            {
                new Tasks
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description for Task 1"
                },
                new Tasks
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2"
                },
                new Tasks
                {
                    Id = 3,
                    Title = "Task 3",
                    Description = "Description for Task 3"
                },
                new Tasks
                {
                    Id = 4,
                    Title = "Task 4",
                    Description = "Description for Task 4"
                },
                new Tasks
                {
                    Id = 5,
                    Title = "Task 5",
                    Description = "Description for Task 5"
                }
            };

            return tasks;
        }

        private List<SubTasks> MockSubTasks()
        {
            return new List<SubTasks>
            {
                new SubTasks
                {
                    Id = 1,
                    Title = "Subtask1",
                    Description = "Mock Task 1",
                    TaskId = 1
                },
                new SubTasks
                {
                    Id = 2,
                    Title = "Subtask2",
                    Description = "Mock Task 2",
                    TaskId = 2 
                },
                new SubTasks
                {
                    Id = 3,
                    Title = "Subtask3",
                    Description = "Mock Task 3",
                    TaskId = 3
                }
            };
        }

        [SetUp]
        public void Setup()
        {
            _taskService = Substitute.For<ITaskService>();
            _subTaskService = Substitute.For<ISubTaskService>();
        }

        [Test]
        public void GetAllForUser_ValidResult_ShouldReturnListOfTasks()
        {
            //Arrange
            var taskList = MockTasks();

            _taskService.GetAllForUser(3).Returns(taskList);

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = controller.GetAllTasksForUser(3).Result;

            //Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetAllForUser_InValidResult_ShouldReturnlistOfTasks()
        {
            //Arrange
            var taskList = MockTasks();

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = controller.GetAllTasksForUser(7).Result;

            //Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));

        }

        [Test]
        public void GetTaskByID_ValidResult_ShouldReturnTask()
        {
            //Arrange
            var taskList = MockTasks();

            var returnTask = taskList.Where(i => i.Id == 3).First();
            _taskService.GetTaskById(3).Returns(returnTask);

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = controller.GetTaskById(3).Result;

            //Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetTaskByID_InValidResult_ShouldReturnTask()
        {
            //Arrange
            var taskList = MockTasks();

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = controller.GetTaskById(8).Result;

            //Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task CreateTask_ValidResult_ShouldCreateTask()
        {
                // Arrange
                var controller = CreateController(_taskService, _subTaskService, _userManager);

                var createTaskDTO = new CreateTaskDTO
                {
                    Title = "Sample Task",
                    Description = "Sample Description",
                    DueDate = DateTime.Now.AddDays(1)
                };

                var task = new Tasks
                {
                    Title = createTaskDTO.Title,
                    Description = createTaskDTO.Description,
                    DueDate = createTaskDTO.DueDate,
                    UserId = 0
                };

                _taskService.CreateTask(Arg.Any<Tasks>()).Returns(Task.CompletedTask);

                // Act
                var result = await controller.CreateTask(createTaskDTO);

                // Assert
                Assert.IsInstanceOf<CreatedAtActionResult>(result);
                var createdAtActionResult = (CreatedAtActionResult)result;
                Assert.That(((Tasks)createdAtActionResult.Value).Id, Is.EqualTo(task.Id));
                Assert.That(((Tasks)createdAtActionResult.Value).Title, Is.EqualTo(task.Title));
                Assert.That(((Tasks)createdAtActionResult.Value).Description, Is.EqualTo(task.Description));
        }

        [Test]
        public async Task CreateTask_InValidResult_ShouldCreateTask()
        {
            // Arrange
            var controller = CreateController(_taskService, _subTaskService, _userManager);

            var createTaskDTO = new CreateTaskDTO
            {
                Title = "",
                Description = "Sample Description",
                DueDate = DateTime.Now.AddDays(1)
            };

            var task = new Tasks
            {
                Title = createTaskDTO.Title,
                Description = createTaskDTO.Description,
                DueDate = createTaskDTO.DueDate,
                UserId = 0
            };

            _taskService.CreateTask(Arg.Any<Tasks>()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.CreateTask(createTaskDTO);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.That(((Tasks)createdAtActionResult.Value).Id, Is.EqualTo(task.Id));
            Assert.That(((Tasks)createdAtActionResult.Value).Title, Is.EqualTo(task.Title));
            Assert.That(((Tasks)createdAtActionResult.Value).Description, Is.EqualTo(task.Description));
        }

        [Test]
        public async Task UpdateTask_ValidResult_ReturnsOkResult()
        {
            //Arrange
            var controller = CreateController(_taskService, _subTaskService, _userManager);

            int taskId = 1;
            var updateDateTaskDto = new UpdateTaskDTO
            {
                Title = "Update Title",
                Description = "Updated Description",
                DueDate = DateTime.Now.AddDays(2)
            };

            var task = new Tasks
            {
                Id = taskId,
                Title = updateDateTaskDto.Title,
                Description = updateDateTaskDto.Description,
                DueDate = updateDateTaskDto.DueDate,
            };

            _taskService.GetTaskById(taskId).Returns(task);

            //Act
            var result = await controller.UpdateTask(taskId, updateDateTaskDto);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateTask_InValidResult_ReturnsNotFoundResult()
        {
            //Arrange
            var controller = CreateController(_taskService, _subTaskService, _userManager);

            int taskId = 100;

            _taskService.GetTaskById(taskId).Returns((Tasks)null);

            //Act
            var result = await controller.UpdateTask(taskId, new UpdateTaskDTO());

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteTask_ValidResult_ReturnsOkResult()
        {

            //Arrange
            var controller = CreateController(_taskService, _subTaskService, _userManager);

            int taskId = 1;
           
            var task = new Tasks
            {
                Id = taskId,
                Title = "Sample Task",
                Description = "Sample task Description",
                DueDate = DateTime.Now.AddDays(2),
            };

            _taskService.GetTaskById(taskId).Returns(task);

            //Act
            var result = await controller.DeleteTask(taskId);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteTask_InValidResult_ReturnsNotFoundResult()
        {

            //Arrange
            var controller = CreateController(_taskService, _subTaskService, _userManager);

            int taskId = 100;

            _taskService.GetTaskById(taskId).Returns((Tasks)null);

            //Act
            var result = await controller.DeleteTask(taskId);

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetSubTaskForTask_ValidResult_ShouldReturnTask()
        {
            //Arrange
            var subTaskList = MockSubTasks();

            int subTaskId = 1;
            _taskService.GetSubTasksForTask(subTaskId).Returns(subTaskList);

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = controller.GetSubTasksForTask(subTaskId).Result;

            //Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetSubTaskForTask_InValidResult_ShouldReturnTask()
        {
            // Arrange
            var subTaskList = MockSubTasks();

            int subTaskId = 50;
            _taskService.GetSubTasksForTask(subTaskId).Returns((List<SubTasks>)null);

            // Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = controller.GetSubTasksForTask(subTaskId).Result;

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task CreateSubTask_ValidResult_ShouldCreateSubTask()
        {
            //Arrange
            int subTaskId = 4;

            var createSubTaskDTO = new CreateSubTaskDTO
            {
                Title = "New SubTask",
                Description = "New Description",
                DueDate = DateTime.Now.AddDays(1),
            };

            var subTask = new SubTasks
            {
                Title = createSubTaskDTO.Title,
                Description = createSubTaskDTO.Description,
                DueDate = createSubTaskDTO.DueDate,
                TaskId = subTaskId
            };

            _taskService.CreateSubTask(Arg.Any<SubTasks>()).Returns(Task.CompletedTask);

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = await controller.CreateSubTask(subTaskId, createSubTaskDTO);

            //Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.That(((SubTasks)createdAtActionResult.Value).Id, Is.EqualTo(subTask.Id));
            Assert.That(((SubTasks)createdAtActionResult.Value).Title, Is.EqualTo(subTask.Title));
            Assert.That(((SubTasks)createdAtActionResult.Value).Description, Is.EqualTo(subTask.Description));
        }

        [Test]
        public async Task CreateSubTask_InValidResult_ShouldCreateSubTask()
        {
            //Arrange
            int subTaskId = 4;

            var createSubTaskDTO = new CreateSubTaskDTO
            {
                Title = "",
                Description = "New Description",
                DueDate = DateTime.Now.AddDays(1),
            };

            var subTask = new SubTasks
            {
                Title = createSubTaskDTO.Title,
                Description = createSubTaskDTO.Description,
                DueDate = createSubTaskDTO.DueDate,
                TaskId = subTaskId
            };

            _taskService.CreateSubTask(Arg.Any<SubTasks>()).Returns(Task.CompletedTask);

            //Act
            var controller = CreateController(_taskService, _subTaskService, _userManager);
            var result = await controller.CreateSubTask(subTaskId, createSubTaskDTO);

            //Assert
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.That(((SubTasks)createdAtActionResult.Value).Id, Is.EqualTo(subTask.Id));
            Assert.That(((SubTasks)createdAtActionResult.Value).Title, Is.EqualTo(subTask.Title));
            Assert.That(((SubTasks)createdAtActionResult.Value).Description, Is.EqualTo(subTask.Description));
        }
    }
    }


