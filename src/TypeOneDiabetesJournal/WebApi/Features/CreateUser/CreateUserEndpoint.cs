using DataLayer.Data;
using Microsoft.AspNetCore.Identity;
using Services;

namespace WebApi.Features.CreateUser
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
    {
        public CreateUserEndpoint(IUserService userService)
        {
            this.UserService = userService;
        }

        public IUserService UserService { get; }

        public override void Configure()
        {
            this.Verbs(Http.POST);
            this.Routes("api/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequest request, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                this.AddError("Email and password are required.");
                await this.SendErrorsAsync(cancellation: ct).ConfigureAwait(false);
                return;
            }

            User newUser = await this.UserService.AddUser(request.Email, request.Password).ConfigureAwait(false);

            var response = new CreateUserResponse
            {
                Email = $"{newUser.Email}",
            };

            await SendAsync(response).ConfigureAwait(false);
        }
    }
}
