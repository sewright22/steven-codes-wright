﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(JournalEntry), Schema = "FoodJournal")]
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Logged { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
    }
}