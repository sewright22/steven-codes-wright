namespace WebApi.Models
{
    public class DoseModel
    {
        public int Extended { get; set; }
        public int UpFront { get; set; }
        public int TimeOffset { get; set; }
        public decimal TimeExtended { get; set; }
        public decimal InsulinAmount { get; set; }
        public int Id { get; set; }
    }
}