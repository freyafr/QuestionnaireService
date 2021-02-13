using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Dto
{
    public class AnswerOptionDto
    {
        public int? AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int OrderNumber { get; set; }
        public int AnswerType { get; set; }
        public int ItemType { get; set; }
        public Dictionary<string, string> Texts { get; set; }
    }
}
