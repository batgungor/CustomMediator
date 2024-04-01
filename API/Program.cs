using API.Business.Features.Users.CreateUser;
using API.CustomMediator;
using API.CustomMediator.Decorators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<CreateUserCommandHandler>();

//builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingDecorator<,>));
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.Decorate<IMediator, LoggingDecorator>();
//builder.Services.AddScoped<IHandler<CreateUserCommand, CreateUserCommandResult>, CreateUserCommandHandler>();

var assembly = typeof(CreateUserCommand).Assembly;
var handlers = assembly.GetTypes().Where(t =>
    t.IsClass &&
    !t.IsGenericTypeDefinition &&
    t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)));

foreach (var handler in handlers)
{
    var handlerInterface = handler.GetInterfaces().First();
    var requestType = handlerInterface.GetGenericArguments()[0];
    var responseType = handlerInterface.GetGenericArguments()[1];
    var handlerType = typeof(IHandler<,>).MakeGenericType(requestType, responseType);
    builder.Services.AddScoped(handlerType, handler);
}

/*builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
});*/


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
