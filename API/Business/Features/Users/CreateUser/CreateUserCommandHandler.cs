using API.CustomMediator;

namespace API.Business.Features.Users.CreateUser
{
    public class CreateUserCommandHandler : IHandler<CreateUserCommand, CreateUserCommandResult>
    {
        public CreateUserCommandHandler()
        {
            //gerekli DI uygulamaları yapılacaktır.
        }

        public async Task<CreateUserCommandResult> Handle(CreateUserCommand requestn)
        {
            /*
             * 
             * gerekli işlemler burada yapılacaktır.
             * 
             * 
             */
            return new CreateUserCommandResult() { Success = true };
        }
    }
}
