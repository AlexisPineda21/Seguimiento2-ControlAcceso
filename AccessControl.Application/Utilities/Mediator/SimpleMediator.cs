using AccessControl.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System.Reflection;

namespace AccessControl.Application.Utilities.Mediator
{
    public class SimpleMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public SimpleMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Type handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            object handler = ResolveHandler(handlerType, request);

            await ValidateRequestAsync(request);

            MethodInfo handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle))!;

            return await (Task<TResponse>)handleMethod.Invoke(handler, [request])!;
        }

        public async Task Send(IRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            Type handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
            object handler = ResolveHandler(handlerType, request);

            await ValidateRequestAsync(request);

            MethodInfo handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest>.Handle))!;

            await (Task)handleMethod.Invoke(handler, [request])!;
        }

        private object ResolveHandler(Type handlerType, object request)
        {
            object? handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
            {
                throw new MediatorException($"No se encontró un caso de uso registrado para {request.GetType().Name}.");
            }

            return handler;
        }

        private async Task ValidateRequestAsync(object request)
        {
            Type requestType = request.GetType();
            Type validatorType = typeof(IValidator<>).MakeGenericType(requestType);

            object? validator = _serviceProvider.GetService(validatorType);

            if (validator is null)
            {
                return;
            }

            MethodInfo validateMethod = validatorType.GetMethod(nameof(IValidator<object>.ValidateAsync), [requestType, typeof(CancellationToken)])!;

            Task<ValidationResult> validationTask = (Task<ValidationResult>)validateMethod.Invoke(validator, [request, CancellationToken.None])!;
            ValidationResult validationResult = await validationTask;

            if (!validationResult.IsValid)
            {
                throw new CustomValidationException(validationResult.Errors);
            }
        }
    }
}
