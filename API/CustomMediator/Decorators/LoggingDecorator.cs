
namespace API.CustomMediator.Decorators
{
    public class LoggingDecorator : IMediator
    {
        private readonly IMediator _mediator;
        public LoggingDecorator(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            Console.WriteLine("İşlem başlıyor");
            var response = await _mediator.Send(request);
            Console.WriteLine("İşlem tamamlandı");
            return response;
        }
    }
}
