using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ReservationSystem.Base.Services.MediatR.Behaviors.Attributes;

namespace ReservationSystem.Base.Services.MediatR.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validationFactory;

        public ValidatorBehavior(IValidatorFactory validationFactory)
        {
            _validationFactory = validationFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestType = request.GetType();
            if (!Attribute.IsDefined(requestType, typeof(ValidationRequestAttribute)))
            {
                return await next();
            }

            var validator = _validationFactory.GetValidator(requestType);
            if (validator == null)
            {
                return await next();
            }

            var result = validator.Validate(request);
            if (result != null && !result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return await next();
        }
    }
}
