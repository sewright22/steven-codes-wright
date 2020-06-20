using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TypeOneFoodJournal.Entities
{
    public class UserPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PasswordId { get; set; }
        public virtual User User { get; set; }
        public virtual Password Password { get; set; }
    }
}
