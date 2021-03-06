﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class JournalEntryModel
    {
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public DoseModel Dose { get; set; } 
        public NutritionalInfoModel NutritionalInfo { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}