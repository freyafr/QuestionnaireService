using Microsoft.EntityFrameworkCore;
using QuestionnaireService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly IDataContext _context;
        public DataRepository(IDataContext context)
        {
            _context = context;
        }
        public int GetAnswerOptionCount(int questionId)
        {
           return _context.AnswerOptions.Where(o => o.QuestionId == questionId)
                .Count();
        }

        public IEnumerable<AnswerOption> GetAnswerOptions(int questionId, int? offset, int? limit)
        {
            return _context.AnswerOptions.Where(o => o.QuestionId == questionId)
                .OrderBy(s => s.OrderNumber)
                .Skip(offset ?? 0).Take(limit ?? 50)
                .ToList();
        }

        public IEnumerable<Answer> GetAnswers(int questionId)
        {
            return _context.Answers.Where(o => o.QuestionId == questionId).ToList();                
        }

        public IEnumerable<Question> GetQuestions(int subjectId, int? offset, int? limit)
        {
            return _context.Questions.Where(o => o.SubjectId == subjectId)
                .OrderBy(s => s.OrderNumber)
                .Skip(offset ?? 0).Take(limit ?? 50)
                .ToList();
        }

        public int GetQuestionsCount(int subjectId)
        {
            return _context.Questions.Where(o => o.SubjectId == subjectId).Count();
        }

        public bool PushAnswer(Answer answer)
        {
            return _context.AddAnswer(answer);
        }
    }
}
