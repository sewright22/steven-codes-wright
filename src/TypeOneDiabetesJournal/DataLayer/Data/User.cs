using System;
using System.Collections.Generic;

namespace DataLayer.Data
{
    public partial class User
    {
        public User()
        {
            this.ExternalServices = new HashSet<ExternalService>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;

        public virtual Userpassword? Userpassword { get; set; }
        public virtual ICollection<ExternalService> ExternalServices { get; set; }
        public virtual ICollection<ExternalServiceUser> ExternalServiceUsers { get; set; }
    }
}
