using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public Dictionary<string, string> Texts { get; set; }
        public int ItemType { get; set; }
        public int OrderNumber { get; set; }
        public IEnumerable<Question> QuestionnaireItems { get; set; }

    }
}
