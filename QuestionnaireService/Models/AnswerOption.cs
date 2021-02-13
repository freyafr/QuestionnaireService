using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Models
{
    public class AnswerOption
    {
              
        public int? AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int OrderNumber { get; set; }
        public int AnswerType { get; set; }
        public int ItemType { get; set; }
        public Dictionary<string, string> Texts { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<Answer> QuestionnaireItems { get; set; } = new List<Answer>();
    }
}
