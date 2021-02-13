using QuestionnaireService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Repositories
{
    public interface IDataContext
    {
        IQueryable<AnswerOption> AnswerOptions { get; }
        IQueryable<Answer> Answers { get; }
        IQueryable<Question> Questions { get; }
        bool AddAnswer(Answer answer);
    }
}
