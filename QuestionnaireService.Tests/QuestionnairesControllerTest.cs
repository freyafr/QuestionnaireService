using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuestionnaireService.Controllers;
using QuestionnaireService.Dto;
using QuestionnaireService.Models;
using QuestionnaireService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionnaireService.Tests
{
    [TestClass]
    public class QuestionnairesControllerTest
    {
        private readonly Mock<IDataRepository> _repoMock = new Mock<IDataRepository>();
        [TestInitialize]
        public void Init()
        {
            _repoMock.Setup(e => e.GetQuestionsCount(It.IsAny<int>()))
            .Returns((int c)=>c*2);
            _repoMock.Setup(e => e.GetQuestions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new System.Collections.Generic.List<Question> {
                    new Question
                    {
                        Id=11,
                        OrderNumber=0,
                        AnswerCategoryType = 1,
                        SubjectId = 22,
                        Texts = new Dictionary<string, string>
                        {
                            {"","" }
                        }
                    }
                });
        }
        [TestMethod]
        public void GetQuestionsCountOk()
        {
            var controller = new QuestionnairesController(_repoMock.Object, Mock.Of<ILogger<QuestionnairesController>>());
            int result = controller.GetQuestionsCount(1);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetQuestionsOk()
        {
            var controller = new QuestionnairesController(_repoMock.Object, Mock.Of<ILogger<QuestionnairesController>>());
            IEnumerable<QuestionDto> result = controller.GetQuestions(1);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(11, result.First().QuestionId);
        }

        [TestMethod]
        public void PushAnswerOk()
        {
            var controller = new QuestionnairesController(_repoMock.Object, Mock.Of<ILogger<QuestionnairesController>>());
            IActionResult result = controller.PushAnswer(1,new AnswerDto 
            {
            });
            Assert.IsInstanceOfType(result,typeof(OkResult));
        }

        [TestMethod]
        public void PushAnswerException()
        {
            _repoMock.Setup(e => e.PushAnswer(It.IsAny<Answer>())).Throws(new ArgumentException("ttt"));
            var controller = new QuestionnairesController(_repoMock.Object, Mock.Of<ILogger<QuestionnairesController>>());
            IActionResult result = controller.PushAnswer(1, new AnswerDto
            {
            });
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual("ttt", ((BadRequestObjectResult)result).Value);
        }

        [TestMethod]
        public void PushAnswerModelNotValid()
        {
            _repoMock.Setup(e => e.PushAnswer(It.IsAny<Answer>())).Throws(new ArgumentException("ttt"));
            var controller = new QuestionnairesController(_repoMock.Object, Mock.Of<ILogger<QuestionnairesController>>());
            controller.ModelState.AddModelError("test", "test");
            IActionResult result = controller.PushAnswer(1, new AnswerDto
            {
            });
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));            
        }
    }
}
