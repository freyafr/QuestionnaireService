using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionnaireService.Dto;
using QuestionnaireService.Models;
using QuestionnaireService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionnairesController : ControllerBase
    {      

        private readonly ILogger<QuestionnairesController> _logger;
        private readonly IDataRepository _repository;

        public QuestionnairesController(IDataRepository repository,ILogger<QuestionnairesController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("subjects/{subjectId}/questions/count")]
        public int GetQuestionsCount(int subjectId)
        {
            return _repository.GetQuestionsCount(subjectId);
        }

        [HttpGet]
        [Route("subjects/{subjectId}/questions")]
        public IEnumerable<QuestionDto> GetQuestions(int subjectId, int? offset=0, int? limit=50)
        {
            return _repository.GetQuestions(subjectId, offset, limit)
                .Select(s =>
                new QuestionDto
                {
                    QuestionId = s.Id,
                    SubjectId = s.SubjectId,
                    Texts = s.Texts,
                    OrderNumber = s.OrderNumber,
                    AnswerCategoryType = s.AnswerCategoryType
                }
                );
        }

        [HttpGet]
        [Route("questions/{questionId}/answers/count")]
        public int GetAnswerOptionsCount([FromRoute]int questionId)
        {
            return _repository.GetAnswerOptionCount(questionId);
        }

        [HttpGet]
        [Route("questions/{questionId}/answers")]
        public IEnumerable<AnswerOptionDto> GetAnswerOptions([FromRoute] int questionId, int? offset = 0, int? limit = 50)
        {
            return _repository.GetAnswerOptions(questionId, offset, limit)
                .Select(q =>
                new AnswerOptionDto
                {
                    AnswerId = q.AnswerId,
                    QuestionId = q.QuestionId,
                    OrderNumber = q.OrderNumber,
                    AnswerType = q.AnswerType,
                    ItemType = q.ItemType,
                    Texts = q.Texts,
                }
                );
        }

        [HttpPut]
        [Route("questions/{questionId}/answers")]
        public IActionResult PushAnswer([FromRoute] int questionId,[FromBody] AnswerDto answerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug(string.Join(Environment.NewLine, ModelState.Select(m=>$"{m.Key}-{m.Value.Errors}")));
                return BadRequest(ModelState);
            }
            var answer = new Answer
            {
                QuestionId = questionId,
                AnswerOptionId = answerDto.AnswerId.Value,
                DepartmentId = answerDto.Department,
                UserId = answerDto.UserId
            };
            try
            {
                _repository.PushAnswer(answer);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("questions/{questionId}/answers_results")]
        public IEnumerable<AnswerStatisticsDto> GetAnswerStatistics([FromRoute] int questionId)
        {
            return _repository.GetAnswers(questionId)
                .GroupBy(r => r.AnswerOptionId)
                 .Select(g =>
                 new AnswerStatisticsDto
                 {
                     AnswerId = g.Key,
                     MinValue = g.GroupBy(d=>d.DepartmentId).Min(s=>s.Count()),
                     MaxValue = g.GroupBy(d => d.DepartmentId).Max(s => s.Count()),
                     AvgValue = g.GroupBy(d => d.DepartmentId).Average(s => s.Count())
                 });
        }
    }
}
