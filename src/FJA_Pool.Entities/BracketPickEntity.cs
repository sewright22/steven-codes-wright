using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJA_Pool.Entities
{
    public class BracketPickEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserEmail { get; set; }

        public int? AFCWildCardGame1WinnerId { get; set; }

        public int? AFCWildCardGame2WinnerId { get; set; }

        public int? NFCWildCardGame1WinnerId { get; set; }

        public int? NFCWildCardGame2WinnerId { get; set; }

        public int? AFCDivisionalGame1WinnerId { get; set; }

        public int? AFCDivisionalGame2WinnerId { get; set; }

        public int? NFCDivisionalGame1WinnerId { get; set; }

        public int? NFCDivisionalGame2WinnerId { get; set; }

        public int? AFCChampionshipWinnerId { get; set; }

        public int? NFCChampionshipWinnerId { get; set; }

        public int? SuperBowlWinnerId { get; set; }

        public int Score { get; set; }

        public int MaxScore { get; set; }

        public bool IsMaster { get; set; }
        public string Season { get; set; }
    }
}
