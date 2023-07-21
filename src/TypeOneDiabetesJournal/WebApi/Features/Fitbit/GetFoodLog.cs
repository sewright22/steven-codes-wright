using Core.Requests;
using Core.Responses;
using DataLayer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Services;
using WebApi.Extensions;

namespace WebApi.Features.Fitbit
{
    public class GetFoodLog : Endpoint<FoodLogRequest, FoodLogResponse>
    {
        public GetFoodLog(IFitbitService fitbitService, ApplicationDbContext dbContext)
        {
            FitbitService = fitbitService;
            DbContext = dbContext;
        }

        public IFitbitService FitbitService { get; }
        public ApplicationDbContext DbContext { get; }

        public override void Configure()
        {
            this.Get("api/foodlog");
        }

        public override async Task HandleAsync(FoodLogRequest req, CancellationToken ct)
        {
            string currentUserId = this.User.GetUserId();
            ExternalServiceUser? fitbitUser = this.DbContext.ExternalServiceUsers
                .Include(x => x.AccessToken)
                .FirstOrDefault(x => x.ExternalService.Name == "Fitbit"
                    && x.UserId == Convert.ToInt32(currentUserId));

            if (fitbitUser == null || fitbitUser.AccessToken == null || fitbitUser.AccessToken.Value == null || fitbitUser.ClientId == null)
            {
                this.AddError("User not found");
                await this.SendErrorsAsync(cancellation: ct);
                return;
            }

            var foodLog = await this.FitbitService.GetFoodLog(fitbitUser.AccessToken.Value, fitbitUser.ClientId, req.Date).ConfigureAwait(false);
            await this.SendOkAsync(foodLog);
        }
    }
}
