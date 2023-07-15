using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TokenTypeId { get; set; }
        public DateTimeOffset Expiration { get; set; }
        public string? Value { get; set; }
        public virtual TokenType TokenType { get; set; } = null!;
    }
}
