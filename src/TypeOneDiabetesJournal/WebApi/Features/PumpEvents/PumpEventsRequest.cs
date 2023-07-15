namespace WebApi.Features.PumpEvents
{
    public class PumpEventsRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
