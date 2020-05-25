using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJA_Pool.Entities
{
    public class TeamEntity
    {
        [Key]
        public int Id { get; set; }

        public int? Seed { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public string Conference { get; set; }

        public string Season { get; set; }
    }
}
