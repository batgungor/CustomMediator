namespace API.CustomMediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();

            var responseType = requestType
                .GetInterfaces()
                .First(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IRequest<>))
                .GetGenericArguments()
                .First();

            var handlerType = typeof(IHandler<,>).MakeGenericType(requestType, responseType);
            var handler = _serviceProvider.GetRequiredService(handlerType);
            var response = (Task<TResponse>)handlerType.GetMethod("Handle")?.Invoke(handler, new object[] { request });
            return response;
        }
    }
}
