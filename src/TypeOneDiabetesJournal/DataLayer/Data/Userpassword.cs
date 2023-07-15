using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Data
{
    public partial class Userpassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PasswordId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(PasswordId))]
        public virtual Password? Password { get; set; }
    }
}
