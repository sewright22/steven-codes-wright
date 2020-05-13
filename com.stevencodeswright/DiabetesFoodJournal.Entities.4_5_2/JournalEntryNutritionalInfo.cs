﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DiabetesFoodJournal.Entities._4_5_2
{
    [Table(nameof(JournalEntryNutritionalInfo), Schema = "FoodJournal")]
    public class JournalEntryNutritionalInfo
    {
        [Key]
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int JournalEntryNutritionalInfoId { get; set; }
    }
}
