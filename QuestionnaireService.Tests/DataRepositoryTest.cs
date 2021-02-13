using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuestionnaireService.Models;
using QuestionnaireService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestionnaireService.Tests
{
    [TestClass]
    public class DataRepositoryTest
    {
        private readonly Mock<IDataContext> _contextMock = new Mock<IDataContext>();

        [TestInitialize]
        public void Init()
        {
            _contextMock.Setup(e => e.AnswerOptions)
                .Returns(new List<AnswerOption> {
                    new AnswerOption
                    {
                        AnswerId =1,
                        QuestionId=3,
                        ItemType=2,
                    },
                    new AnswerOption
                    {
                        AnswerId =11,
                        QuestionId=3,
                        ItemType=2,
                    },

                    new AnswerOption
                    {
                         AnswerId =2,
                         QuestionId=2,
                           OrderNumber = 2,
                        ItemType=3,
                    },
                     new AnswerOption
                    {
                         AnswerId =4,
                         QuestionId=2,
                           OrderNumber = 2,
                        ItemType=3,
                    },
                      new AnswerOption
                    {
                         AnswerId =5,
                         QuestionId=2,
                           OrderNumber = 1,
                        ItemType=3,
                    },
                       new AnswerOption
                    {
                         AnswerId =6,
                         QuestionId=2,
                           OrderNumber = 3,
                        ItemType=3,
                    },
                        new AnswerOption
                    {
                         AnswerId =7,
                         QuestionId=2,
                         OrderNumber = 0,
                        ItemType=3,
                    }
                }.AsQueryable());
        }

        [TestMethod]
        public void GetAnswerOptionCount()
        {
            var repo = new DataRepository(_contextMock.Object);
           int result = repo.GetAnswerOptionCount(3);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetAnswerOption()
        {
            var repo = new DataRepository(_contextMock.Object);
            IEnumerable<AnswerOption> result = repo.GetAnswerOptions(2,0,2);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(7, result.First().AnswerId);
            Assert.AreEqual(5, result.Last().AnswerId);
        }
    }
}
