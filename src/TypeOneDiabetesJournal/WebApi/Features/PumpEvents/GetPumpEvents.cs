using Services;

namespace WebApi.Features.PumpEvents
{
    public class GetPumpEvents : Endpoint<PumpEventsRequest, PumpEventsResponse>
    {
        public GetPumpEvents(IInsulinPumpDataService insulinPumpDataService)
        {
            this.InsulinPumpDataService = insulinPumpDataService;
        }
        public IInsulinPumpDataService InsulinPumpDataService { get; }
        public override void Configure()
        {
            this.Get("api/pumpEvents");
            this.AllowAnonymous();
        }
        public override async Task<PumpEventsResponse> ExecuteAsync(PumpEventsRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password) || req.StartDate == null || req.EndDate == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            // login to the pump
            await this.InsulinPumpDataService.Login(req.Username, req.Password);

            var pumpEvents = await this.InsulinPumpDataService.GetInsulinPumpDataAsync(req.StartDate.Value, req.EndDate.Value);
            return new PumpEventsResponse
            {
                ReadingList = pumpEvents,
            };
        }
    }
}
