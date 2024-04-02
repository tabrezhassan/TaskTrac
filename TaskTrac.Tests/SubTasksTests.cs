using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrac.API.Controllers;
using TaskTrac.BLL.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.Tests
{
    public class SubTasksTests
    {
        private ISubTaskService _subTaskService;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetSubTaskById_ValidResult_ShouldReturnListOfSubTasks()
        {
            var tasks = CreateMockSubTasks();


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
            var tasks = CreateMockSubTasks();

            _subTaskService = Substitute.For<ISubTaskService>();

            var controller = CreateController(_subTaskService);
            var result = controller.GetSubTaskById(5).Result;

            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));

        }

        private SubTasksController CreateController(ISubTaskService service)
        {
            return new SubTasksController(service);
        }

        private List<SubTasks> CreateMockSubTasks()
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
    }
}

