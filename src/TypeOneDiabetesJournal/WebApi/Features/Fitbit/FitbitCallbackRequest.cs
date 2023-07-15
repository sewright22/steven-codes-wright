namespace WebApi.Features.Fitbit
{
    public class FitbitCallbackRequest
    {
        public string? Code { get; set; }
        public string? State { get; set; }
    }
}