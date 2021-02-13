using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Models
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }

        [JsonProperty("questionnaireItems")]
        public IEnumerable<Subject> Subjects { get; set; }
    }
}
