namespace TypeOneFoodJournal.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Dose
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal InsulinAmount { get; set; }
        public int UpFront { get; set; }
        public int Extended { get; set; }
        public decimal TimeExtended { get; set; }
        public int TimeOffset { get; set; }
    }
}
