﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class ExternalService
    {
        public ExternalService() 
        {
            this.Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ExternalServiceUser> ExternalServiceUsers { get; set; }
    }
}
