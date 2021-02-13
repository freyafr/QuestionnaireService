using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Models
{
    public class Question
    {
        [JsonProperty("QuestionId")]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Dictionary<string, string> Texts { get; set; }
        public int OrderNumber { get; set; }
        public int AnswerCategoryType { get; set; }
        public IEnumerable<AnswerOption> QuestionnaireItems { get; set; }
    }
}
