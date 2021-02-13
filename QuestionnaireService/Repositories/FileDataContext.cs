using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuestionnaireService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Repositories
{
    public class FileDataContext : IDataContext
    {
        private readonly Questionnaire _internalStorage;

        public FileDataContext(IOptionsMonitor<AppOptions> options)
        {            
            string fileContent = File.ReadAllText(options.CurrentValue.LocalQuestionarieInit);
            _internalStorage = JsonConvert.DeserializeObject<Questionnaire>(fileContent);

        }
        public IQueryable<AnswerOption> AnswerOptions
        {
            get => _internalStorage.Subjects.SelectMany(s => s.QuestionnaireItems).SelectMany(e => e.QuestionnaireItems).AsQueryable();
        }
        public IQueryable<Answer> Answers { get => _internalStorage.Subjects.SelectMany(s => s.QuestionnaireItems)
                .SelectMany(d=>d.QuestionnaireItems).SelectMany(s=>s.QuestionnaireItems).AsQueryable(); }
        public IQueryable<Question> Questions { get => _internalStorage.Subjects.SelectMany(s => s.QuestionnaireItems).AsQueryable(); }

        public bool AddAnswer(Answer answer)
        {
            var question = _internalStorage.Subjects.SelectMany(q => q.QuestionnaireItems).SingleOrDefault(q => q.Id == answer.QuestionId);

            if (question == null)
            {
                throw new ArgumentException($"Question {answer.QuestionId} is not found");
            }

            var answerOption = question.QuestionnaireItems.SingleOrDefault(a => a.AnswerId == answer.AnswerOptionId);
            if (answerOption == null)
            {
                throw new ArgumentException($"Question {answer.AnswerOptionId} is not found");
            }

            answerOption.QuestionnaireItems.Add(answer);
            return true;
        }
    }
}
