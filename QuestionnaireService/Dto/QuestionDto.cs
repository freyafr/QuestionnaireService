using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Dto
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public int SubjectId { get; set; }
        public Dictionary<string, string> Texts { get; set; }
        public int OrderNumber { get; set; }
        public int AnswerCategoryType { get; set; }
        public IEnumerable<AnswerOptionDto> QuestionnaireItems { get; set; }
    }
}
