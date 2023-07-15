using System.Security.Claims;
using System.Security.Cryptography;
using DataLayer.Data;
using Microsoft.Extensions.Options;
using Services;
using Services.External;
using WebApi.Extensions;

namespace WebApi.Features.Fitbit
{
    public class GetFitbitLink : EndpointWithoutRequest
    {
        public GetFitbitLink(IFitbitService fitbitService, IOptions<FitbitApiOptions> fitBitOptions, ApplicationDbContext dbContext)
        {
            this.FitbitService = fitbitService;
            this.DbContext = dbContext;
            this.FitbitOptions = fitBitOptions.Value;
        }

        public IFitbitService FitbitService { get; }
        public ApplicationDbContext DbContext { get; }
        public FitbitApiOptions FitbitOptions { get; }

        public override void Configure()
        {
            this.Get("/api/fitbit/link");
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            var currentUser = this.User;

            var userId = currentUser.GetUserId();

            string state = GenerateRandomState();

            this.AddExternalServiceToUser(userId, "Fitbit", state);

            return this.SendOkAsync(this.FitbitService.BuildAuthorizationUrl(this.FitbitOptions.ClientId, this.FitbitOptions.RedirectUrl, this.FitbitOptions.Scope, state));
        }

        private void AddExternalServiceToUser(string userId, string serviceName, string state)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var id = int.Parse(userId);
            ExternalService externalService = GetExternalService(serviceName);

            var externalServiceUser = this.DbContext.ExternalServiceUsers.FirstOrDefault(x => x.UserId == id);
            if (externalServiceUser == null)
            {
                externalServiceUser = new ExternalServiceUser
                {
                    UserId = id,
                    ExternalService = externalService,
                    State = state,
                };

                this.DbContext.ExternalServiceUsers.Add(externalServiceUser);
            }

            externalServiceUser.State = state;
            this.ClearTokens(externalServiceUser);

            this.DbContext.SaveChanges();
        }

        private ExternalService GetExternalService(string serviceName)
        {
            ExternalService? externalService = this.DbContext.ExternalServices.FirstOrDefault(x => x.Name == serviceName);

            if (externalService == null)
            {
                throw new ArgumentNullException($"{serviceName} is not a external service");
            }

            return externalService;
        }

        private void ClearTokens(ExternalServiceUser externalServiceUser)
        {
            externalServiceUser.AccessToken = null;
            externalServiceUser.RefreshToken = null;
            externalServiceUser.ExternalTokenExpiration = null;
        }

        private static string GenerateRandomState()
        {
            const int byteLength = 16;
            var randomBytes = new byte[byteLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
