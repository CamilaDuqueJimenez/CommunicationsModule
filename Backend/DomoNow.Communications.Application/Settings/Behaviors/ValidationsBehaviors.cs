using DomoNow.Communications.Application.Dtos;
using FluentValidation;
using MediatR;

namespace DomoNow.Communications.Application.Settings.Behaviors
{
    public class ValidationsBehaviors<IRequest, IResponse>(IValidator<IRequest>? pIValidator = null) : IPipelineBehavior<IRequest, ApiResponse<IResponse>> where IRequest : IRequest<ApiResponse<IResponse>>
    {
        private readonly IValidator<IRequest>? _IValidator = pIValidator;
        public async Task<ApiResponse<IResponse>> Handle(IRequest request, RequestHandlerDelegate<ApiResponse<IResponse>> next, CancellationToken cancellationToken)
        {
            if (_IValidator is null)
            {
                return await next(cancellationToken);
            }

            var validationsResult = await _IValidator.ValidateAsync(request, cancellationToken);

            if (validationsResult.IsValid)
            {
                return await next();
            }

            IReadOnlyList<string> validations = [.. validationsResult.Errors.Select(erros => $"{erros.PropertyName}: {erros.ErrorMessage}")];

            return ApiResponse<IResponse>.Failure(validations);
        }
    }
}
