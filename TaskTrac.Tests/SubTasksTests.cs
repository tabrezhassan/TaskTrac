using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.API.Controllers;
using TaskTrac.API.DTO;
using TaskTrac.BLL.Interfaces;
using TaskTrac.BLL.Services;
using TaskTrac.DAL.Models;

namespace TaskTrac.Tests
{
    [TestFixture]
    public class SubTasksTests
    {
        private ISubTaskService _subTaskService;

        private SubTasksController CreateController(ISubTaskService service)
        {
            return new SubTasksController(service);
        }

        private List<SubTasks> MockSubTasks()
        {
            return new List<SubTasks>
            {
                new SubTasks
                {
                    Id = 1,
                    Title = "Subtask1",
                    Description = "Mock Task 1"
                },
                new SubTasks
                {
                    Id = 2,
                    Title = "Subtask2",
                    Description = "Mock Task 2"
                },
                new SubTasks
                {
                    Id = 3,
                    Title = "Subtask3",
                    Description = "Mock Task 3"
                }
            };
        }


        [SetUp]
        public void Setup()
        {
            _subTaskService = Substitute.For<ISubTaskService>();
        }

        [Test]
        public void GetSubTaskById_ValidResult_ShouldReturnListOfSubTasks()
        {
            var tasks = MockSubTasks();


            _subTaskService = Substitute.For<ISubTaskService>();
            var returnTask = tasks.Where(i => i.Id == 1).First();
            _subTaskService.GetSubTaskById(1).Returns(returnTask);

            var controller = CreateController(_subTaskService);
            var result = controller.GetSubTaskById(1).Result;

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

        }

        [Test]
        public void GetSubTaskById_InValidResult_ShouldReturnlistOfSubTasks()
        {
            var tasks = MockSubTasks();

            _subTaskService = Substitute.For<ISubTaskService>();

            var controller = CreateController(_subTaskService);
            var result = controller.GetSubTaskById(5).Result;

            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));

        }

        [Test]
        public async Task UpdateTask_ValidResult_ReturnsOkResult()
        {
            //Arrange
            var controller = CreateController(_subTaskService);

            int subTaskId = 1;
            var updateSubTaskDTO = new UpdateSubTaskDTO
            {
                Title = "Update Title",
                Description = "Updated Description",
                DueDate = DateTime.Now.AddDays(2)
            };

            var subtask = new SubTasks
            {
                Id = subTaskId,
                Title = updateSubTaskDTO.Title,
                Description = updateSubTaskDTO.Description,
                DueDate = updateSubTaskDTO.DueDate,
            };

            _subTaskService.GetSubTaskById(subTaskId).Returns(subtask);

            //Act
            var result = await controller.UpdateSubTask(subTaskId, updateSubTaskDTO);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateSubTask_InValidResult_ReturnsNotFoundResult()
        {
            //Arrange
            var controller = CreateController(_subTaskService);

            int subTaskId = 100;

            _subTaskService.GetSubTaskById(subTaskId).Returns((SubTasks)null);

            //Act
            var result = await controller.UpdateSubTask(subTaskId, new UpdateSubTaskDTO());

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task DeleteTask_ValidResult_ReturnsOkResult()
        {

            //Arrange
            var controller = CreateController(_subTaskService);

            int subTaskId = 1;

            var subTask = new SubTasks
            {
                Id = subTaskId,
                Title = "Sample Task",
                Description = "Sample task Description",
                DueDate = DateTime.Now.AddDays(2),
            };

            _subTaskService.GetSubTaskById(subTaskId).Returns(subTask);

            //Act
            var result = await controller.DeleteSubTask(subTaskId);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.That(okObjectResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteTask_InValidResult_ReturnsNotFoundResult()
        {

            //Arrange
            var controller = CreateController(_subTaskService);

            int subTaskId = 100;

            _subTaskService.GetSubTaskById(subTaskId).Returns((SubTasks)null);

            //Act
            var result = await controller.DeleteSubTask(subTaskId);

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

       
    }
}

