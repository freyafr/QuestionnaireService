using QuestionnaireService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Repositories
{
    public interface IDataRepository
    {
        IEnumerable<Question> GetQuestions(int subjectId, int? offset, int? limit);
        int GetQuestionsCount(int subjectId);

        IEnumerable<AnswerOption> GetAnswerOptions(int questionId, int? offset, int? limit);
        int GetAnswerOptionCount(int questionId);

        bool PushAnswer(Answer answer);
        IEnumerable<Answer> GetAnswers(int questionId);
    }
}
