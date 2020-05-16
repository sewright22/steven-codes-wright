namespace WebApi.Models
{
    public class DoseModel
    {
        public int Extended { get; internal set; }
        public int UpFront { get; internal set; }
        public int TimeOffset { get; internal set; }
        public decimal TimeExtended { get; internal set; }
        public decimal InsulinAmount { get; internal set; }
        public int Id { get; internal set; }
    }
}