﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireService.Dto
{
    public class AnswerStatisticsDto
    {
        public int AnswerId { get; set; }        
        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public double AvgValue { get; set; }
    }
}
