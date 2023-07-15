using System.Net.Http.Headers;
using System.Text;
using DataLayer.Data;
using Microsoft.Extensions.Options;
using Services;
using Services.External;

namespace WebApi.Features.Fitbit
{
    public class FitbitCallbackEndpoint : FastEndpoints.Endpoint<FitbitCallbackRequest>
    {
        public FitbitCallbackEndpoint(IFitbitService fitbitService, IOptions<FitbitApiOptions> apiOptions, ApplicationDbContext dbContext)
        {
            this.FitbitService = fitbitService;
            this.DbContext = dbContext;
            this.ApiOptions = apiOptions.Value;
        }

        public IFitbitService FitbitService { get; }
        public ApplicationDbContext DbContext { get; }
        public FitbitApiOptions ApiOptions { get; }

        public override void Configure()
        {
            this.Get("/auth/fitbit/callback");
            this.AllowAnonymous();
        }

        public override async Task HandleAsync(FitbitCallbackRequest req, CancellationToken ct)
        {
            IResponseToken token = await this.FitbitService.GetAccessToken(this.ApiOptions.ClientId, this.ApiOptions.ClientSecret, req.Code, this.ApiOptions.RedirectUrl);

            var externalServiceUser = this.DbContext.ExternalServiceUsers.FirstOrDefault(x => x.ExternalService.Name == "Fitbit" && x.State == req.State);

            if (externalServiceUser == null)
            {
                throw new ArgumentNullException($"Could not find State for fitbit.");
            }

            this.UpdateTokensForUser(token, externalServiceUser);

            var clientId = await this.FitbitService.GetClientId(token.AccessToken, token.UserId);

            this.UpdateClientIdForExternalServiceUser(externalServiceUser, clientId);

            await this.DbContext.SaveChangesAsync(ct);

            await this.SendOkAsync().ConfigureAwait(false);
        }

        private void UpdateClientIdForExternalServiceUser(ExternalServiceUser externalServiceUser, string clientId)
        {
            externalServiceUser.ClientId = clientId;
        }

        private void UpdateTokensForUser( IResponseToken token, ExternalServiceUser externalServiceUser)
        {

            externalServiceUser.AccessToken = new DataLayer.Data.Token
            {
                TokenType = this.DbContext.TokenTypes.First(x => x.Name == "Access"),
                Value = token.AccessToken,
                Expiration = DateTimeOffset.UtcNow.AddMinutes(token.ExpiresIn),
            };

            externalServiceUser.RefreshToken = new DataLayer.Data.Token
            {
                TokenType = this.DbContext.TokenTypes.First(x => x.Name == "Refresh"),
                Value = token.RefreshToken,
                Expiration = DateTimeOffset.UtcNow.AddMinutes(token.ExpiresIn),
            };
        }
    }
}
