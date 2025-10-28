using DomoNow.Communications.Application.Models;
using FluentValidation;
using MediatR;

namespace DomoNow.Communications.Application.Settings.Behaviors
{
    public class ValidationsBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next(cancellationToken);
            }

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                IReadOnlyList<string> validationErrors = [.. failures.Select(error => error.ErrorMessage)];
                return CreateFailureResponse<TResponse>(validationErrors);
            }

            return await next(cancellationToken);
        }

        private static TResponse CreateFailureResponse<T>(IReadOnlyList<string> errors)
        {
            var responseType = typeof(T);
            if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                var failureMethod = responseType.GetMethod(Commons.Constants.FAILURE, [typeof(string[])]);
                if (failureMethod != null)
                {
                    var result = failureMethod.Invoke(null, [errors.ToArray()]);
                    return (TResponse)result!;
                }
            }
            throw new InvalidOperationException(Commons.Constants.GENERIC_MESSAGE_EXCEPTION(responseType.Name));
        }
    }
}
