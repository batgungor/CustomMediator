using API.CustomMediator;

namespace API.Business.Features.Users.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
