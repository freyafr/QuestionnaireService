using Newtonsoft.Json;
using QuestionnaireService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Dto
{
    public class AnswerDto
    {
        [Required]
        public int? AnswerId { get; set; }       
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        
        [Range(1, 4)]
        public Departments Department { get; set; }
    }
}
