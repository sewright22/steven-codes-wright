namespace AmerFamilyPlayoffs.Data.DataExtensions
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

#nullable enable
    public static class UserExtensions
    {
        public static User? GetUser(this DbSet<User> users, string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}
