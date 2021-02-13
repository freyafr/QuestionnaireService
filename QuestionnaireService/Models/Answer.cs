using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int AnswerOptionId { get; set; }       
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public Departments DepartmentId { get; set; }
    }

    public enum Departments
    {
        Marketing = 1, 
        Sales, 
        Development, 
        Reception
    }
}
